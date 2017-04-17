// TODO: delete folder

import { Component } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'fetchdata',
    templateUrl: './fetchdata.component.html'
})
export class FetchDataComponent {
    public forecasts: WeatherForecast[];
    public standing: any;
    public unitOfMeasure: any;

    constructor(http: Http) {
        http.get('/api/SampleData/WeatherForecasts').subscribe(result => {
            this.forecasts = result.json() as WeatherForecast[];
        });

        http.get('/api/SampleData/GetStandings?chapter=Forta de munca - salariu mediu net&year=2017&yearFraction=1').subscribe(result => {
            this.standing = result.json().data;
            this.unitOfMeasure = result.json().valueColumnCaption;
        });
    }
}

interface WeatherForecast {
    dateFormatted: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}
