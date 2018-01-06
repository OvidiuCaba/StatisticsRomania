import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppModuleShared } from './app.module.shared';
import { AppComponent } from './components/app/app.component';
import { FacebookModule } from 'ngx-facebook';
import { ShareService } from './services/share.service'
import { AmChartsModule } from "@amcharts/amcharts3-angular";

@NgModule({
    bootstrap: [ AppComponent ],
    imports: [
        BrowserModule,
        AppModuleShared,
        FacebookModule.forRoot(),
        AmChartsModule
    ],
    providers: [
        { provide: 'BASE_URL', useFactory: getBaseUrl },
        ShareService
    ]
})
export class AppModule {
}

export function getBaseUrl() {
    return document.getElementsByTagName('base')[0].href;
}
