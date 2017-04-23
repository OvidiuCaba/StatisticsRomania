import { Component } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'fetchdata',
    templateUrl: './fetchdata.component.html',
    host: {
        '(window:resize)': 'onWindowResize($event)',
    }
})
export class FetchDataComponent {
    public standing: any;
    public unitOfMeasure: any;
    public innerWidth: number;
    public largeScreen: boolean;
    public year: number;
    public month: number;
    public monthText: string;
    public indicator: string;
    public months: Map<number, string>;
    public monthsKeys: Array<number>;

    constructor(http: Http) {

        this.InitializeMonths();

        this.monthsKeys = new Array<number>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
        this.indicator = 'Forta de munca - salariu mediu net';
        this.year = 2017;
        this.month = 1;
        this.monthText = this.months[1];
        this.innerWidth = window.innerWidth;
        this.largeScreen = this.innerWidth > 1400;

        http.get('/api/SampleData/GetStandings?chapter=Forta de munca - salariu mediu net&year=2017&yearFraction=1').subscribe(result => {
            this.standing = result.json().data;
            this.unitOfMeasure = result.json().valueColumnCaption;
        });
    }

    onWindowResize(event: Event) {
        this.innerWidth = window.innerWidth;
        this.largeScreen = this.innerWidth > 1400;
    };

    ChangeIndicator(indicator: string) {
        this.indicator = indicator;
    }

    ChangeMonth(month: number) {
        this.month = month;
        this.monthText = this.months[month];
    }

    private InitializeMonths()
    {
        this.months = new Map<number, string>();

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
