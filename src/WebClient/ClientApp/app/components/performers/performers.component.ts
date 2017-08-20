import { Component, OnInit, Pipe, ViewChild, ElementRef } from '@angular/core';
import { Http } from '@angular/http';
import { Cookie } from 'ng2-cookies';

@Component({
    selector: 'performers',
    templateUrl: './performers.component.html',
})
export class PerformersComponent {
    public comparisonType: number;
    public indicators: Array<IndicatorPerformers>;

    private favouriteCountiesCookieKey: string;

    constructor(private http: Http) {
        this.favouriteCountiesCookieKey = 'favouriteCounties';

        this.comparisonType = 2;

        this.LoadData();
    }

    LoadData() {
        this.http.get('/api/IndicatorPerformers/GetIndicatorPerformers' + (this.comparisonType == 2 ? 'ByYear' : ''))
            .subscribe(result => {
                this.indicators = result.json();
                var selectedCounties = Cookie.get(this.favouriteCountiesCookieKey);
                this.indicators.forEach(indicator => indicator.performers.forEach(performer => performer.favourite = selectedCounties.indexOf(performer.county) > -1));
            });
    }

    ChangeComparisonType(comparisonType: number) {
        this.comparisonType = comparisonType;

        this.LoadData();
    }

    ToggleCounty(county: string) {
        var selectedCounties = Cookie.get(this.favouriteCountiesCookieKey);

        if (selectedCounties.indexOf(county) > -1) {
            selectedCounties = selectedCounties.replace(county + ' ', '');
        } else {
            selectedCounties += county + ' ';
        }

        this.indicators.forEach(indicator => indicator.performers.forEach(performer => performer.favourite = selectedCounties.indexOf(performer.county) > -1));

        Cookie.set(this.favouriteCountiesCookieKey, selectedCounties);
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
    favourite: boolean;
}