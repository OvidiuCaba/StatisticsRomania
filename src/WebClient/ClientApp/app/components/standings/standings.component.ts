import { Component } from '@angular/core';
import { Http } from '@angular/http';
import { CookieService } from 'ngx-cookie-service';

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

    private favouriteCountiesCookieKey: string;

    constructor(private http: Http, private cookieService: CookieService) {

        this.favouriteCountiesCookieKey = 'favouriteCounties';

        this.InitializeMonths();

        this.monthsKeys = new Array<number>(-1, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
        this.indicator = 'Forta de munca - salariu mediu net';
        this.year = 2017;
        var selectedYearFraction = -1;
        this.month = selectedYearFraction;
        this.monthText = (<any>this.months)[selectedYearFraction];
        this.innerWidth = window.innerWidth;
        this.largeScreen = this.innerWidth > 1400;

        this.LoadData();
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
        if (year)
            this.year = year;

        this.http.get('/api/Standings/GetStandings?chapter=' + this.indicator + '&year=' + this.year + '&yearFraction=' + this.month).subscribe(result => {
            this.standing = result.json().data;
            var selectedCounties = this.cookieService.get(this.favouriteCountiesCookieKey);
            this.standing.forEach(x => x.favourite = selectedCounties.indexOf(x.county) > -1);
            this.unitOfMeasure = result.json().valueColumnCaption;
            this.CalculateTotal();
        });
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
        if (this.standing === null || this.standing.length == 0)
        {
            this.total = undefined;
            return;
        }
        var total = this.standing.map(x => x.value).reduce((sum, current) => sum + current);
        this.total = (new Array<string>("Comert international - exporturi FOB", "Comert international - importuri CIF", "Comert international - sold FOB/CIF", "Turism - numar turisti",
            "Turism - innoptari", "Forta de munca - efectiv salariati", "Forta de munca - numar someri")).indexOf(this.indicator) > -1 ? total : total / 42;
    }

    private InitializeMonths()
    {
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
}

// TODO: add TsLite to import data types from .NET
interface WeatherForecast {
    dateFormatted: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}
