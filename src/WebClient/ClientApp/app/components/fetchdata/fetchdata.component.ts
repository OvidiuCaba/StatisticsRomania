import { Component } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'fetchdata',
    templateUrl: './fetchdata.component.html',
    host: { '(window:resize)': 'onWindowResize($event)' }
})
export class FetchDataComponent {
    public standing: any;
    public unitOfMeasure: any;
    public innerWidth: number;
    public largeScreen: boolean;

    constructor(http: Http) {
        http.get('/api/SampleData/GetStandings?chapter=Forta de munca - salariu mediu net&year=2017&yearFraction=1').subscribe(result => {
            this.standing = result.json().data;
            this.unitOfMeasure = result.json().valueColumnCaption;
        });
    }

    onWindowResize(event: Event) {
        this.innerWidth = window.innerWidth;
        this.largeScreen = this.innerWidth > 1400;
    };
}

// TODO: add TsLite to import data types from .NET
interface WeatherForecast {
    dateFormatted: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}
