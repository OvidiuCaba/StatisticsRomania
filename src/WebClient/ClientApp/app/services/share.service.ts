import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { FacebookService, InitParams, UIParams, UIResponse } from 'ngx-facebook';
import { Router } from '@angular/router';

@Injectable()
export class ShareService {

    private favouriteCountiesCookieKey: string;

    constructor(private cookieService: CookieService, private facebookService: FacebookService, private router: Router) {
        this.favouriteCountiesCookieKey = 'favouriteCounties';
    }

    public InitService() {
        let initParams: InitParams = {
            appId: '447972592264310',
            xfbml: true,
            version: 'v2.11'
        };

        this.facebookService.init(initParams);
    }

    public GetShareableUrlForPerformers(comparisonType: string) {
        return 'http://statisticiromania.ro' + this.router.url + '?share=true&analysis=' + comparisonType + '&favouriteCounties=' + this.cookieService.get(this.favouriteCountiesCookieKey);
    }

    public GetShareableUrlForCountyDetails(countyId1: number, countyId2: number, chapter: string, needToProcessAllYear: boolean) {
        return 'http://statisticiromania.ro' + this.router.url + '?share=true&countyId1=' + countyId1 + '&countyId2=' + countyId2 + '&chapter=' + chapter + '&needToProcessAllYear=' + needToProcessAllYear;
    }

    public GetShareableUrlForStandings(chapter: string, year: number, yearFraction: number) {
        return 'http://statisticiromania.ro' + this.router.url + '?share=true&chapter=' + chapter + '&year=' + year + '&yearFraction=' + yearFraction + '&favouriteCounties=' + this.cookieService.get(this.favouriteCountiesCookieKey);
    }
}