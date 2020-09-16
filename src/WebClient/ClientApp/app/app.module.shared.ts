import { Component, NgModule, TRANSLATIONS, TRANSLATIONS_FORMAT, LOCALE_ID } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { AboutComponent } from './components/about/about.component';
import { CountyDetailsComponent } from './components/county-details/county-details.component';
import { PerformersComponent } from './components/performers/performers.component';
import { StandingsComponent } from './components/standings/standings.component';
import { Covid19Component } from './components/covid19/covid19.component';
import { CounterComponent } from './components/counter/counter.component';
import { ChartsModule } from 'ng2-charts';
import { CookieService } from 'ngx-cookie-service';
import { HttpModule, JsonpModule } from '@angular/http';
import 'chart.js';
import { MonthsComponent } from './components/shared/months.component';

@NgModule({
    bootstrap: [AppComponent],
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        CountyDetailsComponent,
        PerformersComponent,
        Covid19Component,
        StandingsComponent,
        AboutComponent,
        MonthsComponent
    ],
    imports: [
        CommonModule,
        BrowserModule,
        HttpModule,
        JsonpModule,    // TODO: is this needed?
        RouterModule.forRoot([
            { path: '', redirectTo: 'performerii-lunii', pathMatch: 'full' },
            { path: 'despre-noi', component: AboutComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'statistici-judetene', component: CountyDetailsComponent },
            { path: 'clasamente', component: StandingsComponent },
            { path: 'covid19', component: Covid19Component },
            { path: 'performerii-lunii', component: PerformersComponent },
            { path: '**', redirectTo: 'statistici-judetene' }
        ]),
        FormsModule,
        ReactiveFormsModule,
        NgbModule.forRoot(),
        ChartsModule
    ],
    providers: [
        { provide: LOCALE_ID, useValue: "ro-RO" },
        CookieService
    ]
})
export class AppModuleShared {
}