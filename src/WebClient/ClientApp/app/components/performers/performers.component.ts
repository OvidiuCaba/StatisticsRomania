import { Component, OnInit, Pipe, ViewChild, ElementRef } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'performers',
    templateUrl: './performers.component.html',
})
export class PerformersComponent {
    public indicators: Array<IndicatorPerformers>;

    constructor(private http: Http) {
        this.LoadData();
    }

    LoadData() {
        this.http.get('/api/IndicatorPerformers/GetIndicatorPerformers')
            .subscribe(result => {
                this.indicators = result.json();
            });
    }
}

class IndicatorPerformers {
    name: string;
    comparisonPeriod: string;
    performers: Array<Performer>;
}

class Performer {
    position: number;
    county: string;
    oldValue: number;
    newValue: number;
    valueVariation: string;
}