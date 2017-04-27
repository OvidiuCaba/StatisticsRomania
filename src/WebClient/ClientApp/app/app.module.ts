import { Component, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { UniversalModule } from 'angular2-universal';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { AboutComponent } from './components/about/about.component';
import { CountyDetailsComponent } from './components/county-details/county-details.component';
import { StandingsComponent } from './components/standings/standings.component';
import { CounterComponent } from './components/counter/counter.component';

@NgModule({
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        CountyDetailsComponent,
        StandingsComponent,
        AboutComponent
    ],
    imports: [
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        RouterModule.forRoot([
            { path: '', redirectTo: 'statistici-judetene', pathMatch: 'full' },
            { path: 'despre-noi', component: AboutComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'statistici-judetene', component: CountyDetailsComponent },
            { path: 'clasamente', component: StandingsComponent },
            { path: '**', redirectTo: 'statistici-judetene' }
        ]),
        FormsModule,
        ReactiveFormsModule,
        NgbModule.forRoot()
    ]
})
export class AppModule {
}
