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

    constructor(http: Http) {
        this.indicator = 'Forta de munca - salariu mediu net';
        this.year = 2017;
        this.month = 1;
        this.monthText = 'Ianuarie';
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

    // TODO: make it general
    ChangeMonth(monthText: string) {
        if (monthText == 'Ianuarie') {
            this.month = 1;
            this.monthText = 'Ianuarie';
        } else if
            (monthText == 'Februarie') {
            this.month = 2;
            this.monthText = 'Februarie';
        } else if (monthText == 'Martie') {
            this.month = 3;
            this.monthText = 'Martie';
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
