import { Component, OnInit, Pipe, ViewChild, ElementRef } from '@angular/core';
import { BaseChartDirective } from 'ng2-charts/ng2-charts';
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
    // charts
    public lineChartData: Array<any>;
    public lineChartLabels: Array<any>;
    public lineChartOptions: any;
    public lineChartLegend: boolean;
    public lineChartType: string;
    @ViewChild(BaseChartDirective) chart: BaseChartDirective;

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
                this.county1Text = this.counties1.find(x => x.id == 1)!.name;
                this.counties2 = new Array<ICounty>();
                var defaultCounty2 = new County();
                defaultCounty2.id = 0;
                defaultCounty2.name = '-----------------';
                this.counties2.push(defaultCounty2);
                this.counties1.forEach(x => this.counties2.push(x));
                this.county2 = 0;
                this.county2Text = this.counties2.find(x => x.id == 0)!.name;

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

            this.LoadChart();
        });
    }

    LoadChart() {
        var chartData = JSON.parse(JSON.stringify(this.countyDetails)).sort(this.sortCountyDetails);
        var lineChartData = [
            {
                data: chartData.map((x: any) => x.value),
                label: this.county1Text
            }
        ];
        if (this.county2 > 0) {
            lineChartData.push({
                data: chartData.map((x: any) => x.value2),
                label: this.county2Text
            });
        }
        else {
            // workaround: if I don't put this null object here, the second line would never appear on the graph'
            lineChartData.push({
                data: {},
                label: '-------------'
            });
        }
        this.lineChartData = lineChartData;
        // TODO: determine years dynamically
        this.lineChartLabels = this.needToProcessAllYear ? ['2015', '2016'] : chartData.map((x: any) => x.year + ' ' + x.yearFraction);
        this.lineChartOptions = {
            responsive: true
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

    // TODO: maybe chose some proper color later; grey is not a proper color; so better go with the default for now
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
}

interface ICounty {
    id: number;
    name: string;
}

class County implements ICounty {
    id: number;
    name: string;
}