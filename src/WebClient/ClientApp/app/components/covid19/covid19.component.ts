import { Location, DecimalPipe } from '@angular/common';
import { Component, OnInit, Pipe, ViewChild, ElementRef } from '@angular/core';
import { BaseChartDirective } from 'ng2-charts/ng2-charts';
import { Http } from '@angular/http';
import { ShareService } from '../../services/share.service';
import { Router } from '@angular/router';

@Component({
    selector: 'covid19',
    templateUrl: './covid19.component.html',
    host: {
        '(window:resize)': 'onWindowResize($event)',
    }
})
export class Covid19Component {
    public innerWidth: number;
    public largeScreen: boolean;

    public year: number;
    public month: number;
    public monthText: string;
    public months: { [index: number]: string } = {};
    public monthsKeys: Array<number>;

    public counties: Array<ICounty>;
    public county: number;
    public countyText: string;

    public covidDetails: any;
    public maxMonthForLastYear: string;
    public valueColumnCaption: string;
    public value2ColumnCaption: string;
    public comparisonType: number;
    public needToProcessAllYear: boolean;
    public shareableUrl: string;
    // charts
    public lineChartData: Array<any>;
    public lineChartLabels: Array<any>;
    public lineChartOptions: any;
    public lineChartLegend: boolean;
    public lineChartType: string;
    @ViewChild(BaseChartDirective) chart: BaseChartDirective;

    constructor(private http: Http, private router: Router, private location: Location) {

        this.InitializeMonths();

        this.monthsKeys = new Array<number>(-1, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);

        this.comparisonType = 2;
        this.year = 2020;
        var selectedYearFraction = -1;
        this.month = selectedYearFraction;
        this.monthText = (<any>this.months)[selectedYearFraction];

        this.innerWidth = window.innerWidth;
        this.largeScreen = this.innerWidth > 1400;

        this.GetCounties();
    /* ******************** */
        this.needToProcessAllYear = this.comparisonType == 2;
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

    ChangeMonth(month: number) {
        this.month = month;
        this.monthText = this.months[month];

        this.GetCounties();

        this.LoadData();
    }

    onWindowResize(event: Event) {
        this.innerWidth = window.innerWidth;
        this.largeScreen = this.innerWidth > 1400;
    };

    GetCounties() {

        this.http.get('/api/Covid19/GetCounties?month=' + this.month)
            .subscribe(result => {
                this.counties = result.json();
                this.county = 1;
                this.countyText = this.counties.find(x => x.id == 1)!.name;

                this.LoadData();
            });
    }

/* ******************** */

    ChangeCounty(county: ICounty) {
        this.county = county.id;
        this.countyText = county.name;

        this.LoadData();
    }

    ChangeComparisonType(comparisonType: number) {
        this.needToProcessAllYear = comparisonType == 2;
        this.comparisonType = comparisonType;

        this.LoadData();
    }

    LoadData() {
        this.http.get('/api/Covid19/GetCovidDetails?countyId=' + this.county + '&month=' + this.month).subscribe(result => {
            this.covidDetails = result.json();
            //this.valueColumnCaption = result.json().valueColumnCaption;
            //this.value2ColumnCaption = result.json().value2ColumnCaption;

            //this.LoadChart();
        });

        this.http.get('/api/Covid19/GetNaxMonthForLastYear').subscribe(result => {
            this.maxMonthForLastYear = this.months[result.json()];
            //this.valueColumnCaption = result.json().valueColumnCaption;
            //this.value2ColumnCaption = result.json().value2ColumnCaption;

            //this.LoadChart();
        });
    }

    LoadChart() {
        var chartData = JSON.parse(JSON.stringify(this.covidDetails)).sort(this.sortCountyDetails);
        var lineChartData = [
            {
                data: chartData.map((x: any) => x.value),
                label: this.countyText
            }
        ];
        this.lineChartData = lineChartData;
        this.lineChartLabels = this.needToProcessAllYear ? chartData.map((x: any) => x.year) : chartData.map((x: any) => x.year + ' ' + x.yearFraction);
        this.lineChartOptions = {
            responsive: true,
            scales: {
                yAxes: [{
                    ticks: {
                        callback: (dataLabel: any, index: any) => {
                            let decimalPipe = new DecimalPipe('ro-RO');
                            return decimalPipe.transform(dataLabel, '1.0-0');
                        }
                    }
                }],
            },
            title: {
                display: true,
                text: ""
            }
        };
        this.lineChartLegend = true;
        this.lineChartType = 'line';

        setTimeout(() => {
            if (this.chart && this.chart.chart && this.chart.chart.config) {
                // TODO: remove this hack once the ng2-charts fixes this issue
                this.chart.chart.config.data.labels = this.lineChartLabels;
                this.chart.chart.update();
            }
        });
    }

    private sortCountyDetails(n1: any, n2: any): any {
        if (n1.year > n2.year)
            return 1;
        else if (n1.year === n2.year) {
            if (n1.yearFraction > n2.yearFraction)
                return 1;
            else if (n1.yearFraction === n2.yearFraction)
                return 0;
            else
                return -1;
        } else
            return -1;
    }
}

interface ICounty {
    id: number;
    name: string;
    cases: number;
    color: string;
}

class County implements ICounty {
    id: number;
    name: string;
    cases: number;
    color: string;
}