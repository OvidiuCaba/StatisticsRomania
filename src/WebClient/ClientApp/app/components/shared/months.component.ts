import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
    selector: 'months',
    templateUrl: './months.component.html',
    host: {
        '(window:resize)': 'onWindowResize($event)',
    }
})
export class MonthsComponent {
    @Input() month: number;
    @Output() onChangeMonth = new EventEmitter<number>();
    public innerWidth: number;
    public largeScreen: boolean;
    public monthText: string;
    public months: { [index: number]: string } = {};
    public monthsKeys: Array<number>;

    constructor() {

        this.InitializeMonths();

        this.monthsKeys = new Array<number>(-1, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12);
        var selectedYearFraction = -1;
        this.month = selectedYearFraction;
        this.monthText = (<any>this.months)[selectedYearFraction];
        this.innerWidth = window.innerWidth;
        this.largeScreen = this.innerWidth > 1400;
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
        this.onChangeMonth.emit(month);
        this.month = month;
        this.monthText = this.months[month];
    }

    onWindowResize(event: Event) {
        this.innerWidth = window.innerWidth;
        this.largeScreen = this.innerWidth > 1400;
    };
}