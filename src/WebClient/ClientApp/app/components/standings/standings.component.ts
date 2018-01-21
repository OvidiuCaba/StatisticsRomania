// KB:
// https://www.amcharts.com/kbase/your-first-ammap/
// https://github.com/amcharts/amcharts3-angular2

import { Location } from '@angular/common';
import { Component } from '@angular/core';
import { Http } from '@angular/http';
import { CookieService } from 'ngx-cookie-service';
import { ShareService } from '../../services/share.service';
import { Router } from '@angular/router';
import { AmChartsService, AmChart } from "@amcharts/amcharts3-angular";

@Component({
    selector: 'standings',
    templateUrl: './standings.component.html',
    host: {
        '(window:resize)': 'onWindowResize($event)',
    }
})
export class StandingsComponent {
    public standing: Array<any>;
    public unitOfMeasure: any;
    public innerWidth: number;
    public largeScreen: boolean;
    public year: number;
    public month: number;
    public monthText: string;
    public indicator: string;
    public months: { [index: number]: string } = {};
    public monthsKeys: Array<number>;
    public total?: number;
    public shareableUrl: string;

    private favouriteCountiesCookieKey: string;
    private map: AmChart;

    constructor(private http: Http, private cookieService: CookieService, private shareService: ShareService, private router: Router, private location: Location, private AmCharts: AmChartsService) {

        this.favouriteCountiesCookieKey = 'favouriteCounties';
        // TODO: remove this if possible
        this.shareService.InitService();

        this.InitializeMonths();

        this.monthsKeys = new Array<number>(-1, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);

        var queryParams = this.router.parseUrl(this.router.url).queryParams;

        if (queryParams['share'] == 'true') {
            this.indicator = queryParams['chapter'].replace(new RegExp("\\+", "g"), ' ');
            this.year = +queryParams['year'];
            this.month = +queryParams['yearFraction'];
            this.monthText = (<any>this.months)[this.month];
        }
        else {
            this.indicator = 'Forta de munca - salariu mediu net';
            this.year = 2017;
            var selectedYearFraction = -1;
            this.month = selectedYearFraction;
            this.monthText = (<any>this.months)[selectedYearFraction];
        }

        this.innerWidth = window.innerWidth;
        this.largeScreen = this.innerWidth > 1400;

        this.LoadData();

        this.location.go('/clasamente');
    }

    onWindowResize(event: Event) {
        this.innerWidth = window.innerWidth;
        this.largeScreen = this.innerWidth > 1400;
    };

    ChangeIndicator(indicator: string) {
        this.indicator = indicator;

        this.LoadData();
    }

    ChangeMonth(month: number) {
        this.month = month;
        this.monthText = this.months[month];

        this.LoadData();
    }

    LoadData(year?: number) {
        this.shareableUrl = this.shareService.GetShareableUrlForStandings(this.indicator, this.year, this.month);
        this.shareService.InitService();    // refresh the share button URL
        // TODO: Fix the flickering of the Share button
        // This does not work [yet]
        //var ctrl = document.getElementById('fb-share-button');
        //(<any>window).FB.XFBML.parse(ctrl);

        if (year)
            this.year = year;

        this.http.get('/api/Standings/GetStandings?chapter=' + this.indicator + '&year=' + this.year + '&yearFraction=' + this.month).subscribe(result => {
            this.standing = result.json().data;
            var selectedCounties = this.cookieService.get(this.favouriteCountiesCookieKey);
            this.standing.forEach(x => x.favourite = selectedCounties.indexOf(x.county) > -1);
            this.unitOfMeasure = result.json().valueColumnCaption;
            this.CalculateTotal();

            // Ugly workaround for a bug; the map is not displayed at first load
            setTimeout(() => {
                this.RefreshMap();
            }, 300);
            
        });
    }

    RefreshMap() {
        if (this.standing.length == 0)
            return;

        var labelsShiftedX: { [index: string]: number } = {
            "RO-CJ": 5,
            "RO-CT": 10,
            "RO-MH": 20,
            "RO-MM": -10,
            "RO-NT": -5,
            "RO-OT": 10,
            "RO-SM": -10,
            "RO-TL": -20,
        };
        var labelsShiftedY: { [index: string]: number } = {
            "RO-AB": -10,
            "RO-BN": -10,
            "RO-CL": -5,
            "RO-IL": -10,
            "RO-IS": -5,
            "RO-TM": -10,
            "RO-SV": -10,
            "RO-VS": -10,
        };

        var offset = 0;
        this.map.dataProvider.images = [];
        for (var x in this.map.dataProvider.areas) {
            var area = this.map.dataProvider.areas[x];
            area.value = this.standing.filter(x => x.county == area.title)[0].value;
            var image = {
                "latitude": this.map.getAreaCenterLatitude(area),
                "longitude": this.map.getAreaCenterLongitude(area),
                "label": area.callout ? '' : area.title + '\n' + area.value,
                "title": area.title,
                "linkToObject": area,
                "labelShiftX": labelsShiftedX[area.id] || 0,
                "labelShiftY": labelsShiftedY[area.id] || 0,
                "groupId": area.id
            };
            this.map.dataProvider.images.push(image);

            if (area.callout) {
                var image2 = {
                    "latitude": 47.5 + offset,
                    "longitude": 28,
                    "label": area.title + ' ' + area.value,
                    "title": area.title,
                    "type": "circle",
                    //"labelColor": "#000",
                    "labelShiftX": 50,
                    "width": 22,
                    "height": 22,
                    "groupId": area.id
                };
                this.map.dataProvider.images.push(image2);

                offset += 0.3;
            }
        }
        var values = this.standing.map(x => x.value);
        this.map.valueLegend.minValue = values.reduce((min, current) => Math.min(min, current));
        this.map.valueLegend.maxValue = values.reduce((max, current) => Math.max(max, current));
        this.map.validateData();
    }
    
    ToggleCounty(county: string) {
        var selectedCounties = this.cookieService.get(this.favouriteCountiesCookieKey);

        if (selectedCounties.indexOf(county) > -1) {
            selectedCounties = selectedCounties.replace(county + ' ', '');
            this.standing.filter(x => x.county == county).forEach(x => x.favourite = false);
        } else {
            selectedCounties += county + ' ';
            this.standing.filter(x => x.county == county).forEach(x => x.favourite = true);
        }

        this.cookieService.set(this.favouriteCountiesCookieKey, selectedCounties);
    }

    RemoveAllCountiesFromFavourites() {
        this.cookieService.delete(this.favouriteCountiesCookieKey);

        this.standing.forEach(x => x.favourite = false);
    }

    private CalculateTotal() {
        if (this.standing === null || this.standing.length == 0) {
            this.total = undefined;
            return;
        }
        var total = this.standing.map(x => x.value).reduce((sum, current) => sum + current);
        this.total = (new Array<string>("Comert international - exporturi FOB", "Comert international - importuri CIF", "Comert international - sold FOB/CIF", "Turism - numar turisti",
            "Turism - innoptari", "Forta de munca - efectiv salariati", "Forta de munca - numar someri")).indexOf(this.indicator) > -1 ? total : total / 42;
    }

    private InitializeMonths() {
        this.months[-1] = "Tot anul";
        this.months[1] = "Ianuarie";
        this.months[2] = "Februarie";
        this.months[3] = "Martie";
        this.months[4] = "Aprilie";
        this.months[5] = "Mai";
        this.months[6] = "Iunie";
        this.months[7] = "Iulie";
        this.months[8] = "August";
        this.months[9] = "Septembrie";
        this.months[10] = "Octombrie";
        this.months[11] = "Noiembrie";
        this.months[12] = "Decembrie";
    }

    ngAfterViewInit() {
        this.map = this.AmCharts.makeChart("chartdiv", {
            "type": "map",
            "dragMap": false,
            "zoomOnDoubleClick": false,
            "zoomControl": {
                "zoomControlEnabled": false
            },
            "theme": "light",
            "dataProvider": {
                "mapURL": "romaniaHigh.svg",
                "getAreasFromMap": true,
                "areas": [
                    { "id": "RO-B", "title": "Bucuresti", "callout": true },
                    { "id": "RO-IF", "title": "Ilfov", "callout": true },
                ]
            },
            "imagesSettings": {
                "labelPosition": "middle",
                "labelFontSize": 8
            },
            "valueLegend": {
                "right": 10,
                "minValue": "",
                "maxValue": ""
            },
        });

        //var labelsShiftedX: { [index: string]: number } = {
        //    "RO-CJ": 5,
        //    "RO-CT": 10,
        //    "RO-MH": 20,
        //    "RO-MM": -10,
        //    "RO-NT": -5,
        //    "RO-OT": 10,
        //    "RO-SM": -10,
        //    "RO-TL": -20,
        //};
        //var labelsShiftedY: { [index: string]: number } = {
        //    "RO-AB": -10,
        //    "RO-BN": -10,
        //    "RO-CL": -5,
        //    "RO-IL": -10,
        //    "RO-IS": -5,
        //    "RO-TM": -10,
        //    "RO-SV": -10,
        //    "RO-VS": -10,
        //};

        //var offset = 0;
        //this.map.dataProvider.images = [];
        //for (var x in this.map.dataProvider.areas) {
        //    var area = this.map.dataProvider.areas[x];
        //    //try {
        //    //    area.value = this.standing.filter(x => x.county == area.title)[0].value;
        //    //}
        //    //catch (exception) {
        //    //    debugger;
        //    //}
        //    var image = {
        //        "latitude": this.map.getAreaCenterLatitude(area),
        //        "longitude": this.map.getAreaCenterLongitude(area),
        //        "label": area.callout ? '' : area.title/* + '\n' + area.value*/,
        //        "title": area.title,
        //        "linkToObject": area,
        //        "labelShiftX": labelsShiftedX[area.id] || 0,
        //        "labelShiftY": labelsShiftedY[area.id] || 0,
        //        "groupId": area.id
        //    };
        //    this.map.dataProvider.images.push(image);

        //    if (area.callout) {
        //        var image2 = {
        //            "latitude": 47.5 + offset,
        //            "longitude": 28,
        //            "label": area.title/* + ' ' + area.value*/,
        //            "title": area.title,
        //            "type": "circle",
        //            //"labelColor": "#000",
        //            "labelShiftX": 50,
        //            "width": 22,
        //            "height": 22,
        //            "groupId": area.id
        //        };
        //        this.map.dataProvider.images.push(image2);

        //        offset += 0.3;
        //    }
        //}
        ////var values = this.standing.map(x => x.value);
        ////this.map.valueLegend.minValue = values.reduce((min, current) => Math.min(min, current));
        ////this.map.valueLegend.maxValue = values.reduce((max, current) => Math.max(max, current));
        //this.map.validateData();
    }

    ngOnDestroy() {
        if (this.map) {
            this.AmCharts.destroyChart(this.map);
        }
    }
}

// TODO: add TsLite to import data types from .NET
interface WeatherForecast {
    dateFormatted: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}
