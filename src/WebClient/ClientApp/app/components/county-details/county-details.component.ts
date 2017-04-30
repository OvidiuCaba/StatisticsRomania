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
    public countyDetails: any;
    public valueColumnCaption: string;
    public value2ColumnCaption: string;
    public comparisonType: number;
    public needToProcessAllYear: boolean;

    constructor(private http: Http) {

        this.GetCounties();

        this.indicator = 'Forta de munca - salariu mediu net';
        this.comparisonType = 1;
        this.needToProcessAllYear = this.comparisonType == 2;
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

                this.LoadData();
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

    ChangeComparisonType(comparisonType: number) {
        this.needToProcessAllYear = comparisonType == 2;
        this.comparisonType = comparisonType;

        this.LoadData();
    }

    LoadData() {
        this.http.get('/api/CountyDetails/GetCountyDetails?countyId=' + this.county1 + '&countyId2=' + this.county2 + '&chapter=' + this.indicator + '&needToProcessAllYear=' + this.needToProcessAllYear).subscribe(result => {
            this.countyDetails = result.json().data;
            this.valueColumnCaption = result.json().valueColumnCaption;
            this.value2ColumnCaption = result.json().value2ColumnCaption;
        });
    }

    //public lineChartData: Array<any> = [
    //    { data: [65, 59, 80, 81, 56, 55, 40], label: 'Series A' },
    //    { data: [28, 48, 40, 19, 86, 27, 90], label: 'Series B' },
    //    { data: [18, 48, 77, 9, 100, 27, 40], label: 'Series C' }
    //];
    //public lineChartLabels: Array<any> = ['January', 'February', 'March', 'April', 'May', 'June', 'July'];
    //public lineChartOptions: any = {
    //    responsive: true
    //};
    //public lineChartColors: Array<any> = [
    //    { // grey
    //        backgroundColor: 'rgba(148,159,177,0.2)',
    //        borderColor: 'rgba(148,159,177,1)',
    //        pointBackgroundColor: 'rgba(148,159,177,1)',
    //        pointBorderColor: '#fff',
    //        pointHoverBackgroundColor: '#fff',
    //        pointHoverBorderColor: 'rgba(148,159,177,0.8)'
    //    },
    //    { // dark grey
    //        backgroundColor: 'rgba(77,83,96,0.2)',
    //        borderColor: 'rgba(77,83,96,1)',
    //        pointBackgroundColor: 'rgba(77,83,96,1)',
    //        pointBorderColor: '#fff',
    //        pointHoverBackgroundColor: '#fff',
    //        pointHoverBorderColor: 'rgba(77,83,96,1)'
    //    },
    //    { // grey
    //        backgroundColor: 'rgba(148,159,177,0.2)',
    //        borderColor: 'rgba(148,159,177,1)',
    //        pointBackgroundColor: 'rgba(148,159,177,1)',
    //        pointBorderColor: '#fff',
    //        pointHoverBackgroundColor: '#fff',
    //        pointHoverBorderColor: 'rgba(148,159,177,0.8)'
    //    }
    //];
    //public lineChartLegend: boolean = true;
    //public lineChartType: string = 'line';

    //public randomize(): void {
    //    let _lineChartData: Array<any> = new Array(this.lineChartData.length);
    //    for (let i = 0; i < this.lineChartData.length; i++) {
    //        _lineChartData[i] = { data: new Array(this.lineChartData[i].data.length), label: this.lineChartData[i].label };
    //        for (let j = 0; j < this.lineChartData[i].data.length; j++) {
    //            _lineChartData[i].data[j] = Math.floor((Math.random() * 100) + 1);
    //        }
    //    }
    //    this.lineChartData = _lineChartData;
    //}
}

interface ICounty {
    id: number;
    name: string;
}

class County implements ICounty {
    id: number;
    name: string;
}