import { Component } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'county-details',
    templateUrl: './county-details.component.html',
})
export class CountyDetailsComponent {
    public counties1: Array<ICounty>;
    public counties2: Array<ICounty>;
    public county1: number;
    public county2: number;
    public county1Text: string;
    public county2Text: string;
    public indicator: string;

    constructor(private http: Http) {

        this.GetCounties();

        this.indicator = 'Forta de munca - salariu mediu net';
    }

    GetCounties() {

        this.http.get('/api/CountyDetails/GetCounties')
            .subscribe(result => {
                this.counties1 = result.json();
                this.county1 = 1;
                this.county1Text = this.counties1.find(x => x.id == 1).name;
                this.counties2 = new Array<ICounty>();
                var defaultCounty2 = new County() ;
                defaultCounty2.id = 0;
                defaultCounty2.name = '-----------------';
                this.counties2.push(defaultCounty2);
                this.counties1.forEach(x => this.counties2.push(x));
                this.county2 = 0;
                this.county2Text = this.counties2.find(x => x.id == 0).name;
            });
    }

    ChangeCounty1(county: ICounty) {
        this.county1 = county.id;
        this.county1Text = county.name;

        this.LoadData();
    }

    ChangeCounty2(county: ICounty) {
        this.county2 = county.id;
        this.county2Text = county.name;

        this.LoadData();
    }

    ChangeIndicator(indicator: string) {
        this.indicator = indicator;

        this.LoadData();
    }

    LoadData() {


        //this.http.get('/api/Standings/GetStandings?chapter=' + this.indicator + '&year=' + this.year + '&yearFraction=' + this.month).subscribe(result => {
        //    this.standing = result.json().data;
        //    this.unitOfMeasure = result.json().valueColumnCaption;
        //});
    }
}

interface ICounty {
    id: number;
    name: string;
}

class County implements ICounty {
    id: number;
    name: string;
}