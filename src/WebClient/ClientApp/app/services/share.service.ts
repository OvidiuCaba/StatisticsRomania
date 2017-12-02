import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { FacebookService, InitParams, UIParams, UIResponse } from 'ngx-facebook';
import { Router } from '@angular/router';

@Injectable()
export class ShareService {

    private favouriteCountiesCookieKey: string;

    constructor(private cookieService: CookieService, private facebookService: FacebookService, private router: Router) {
        let initParams: InitParams = {
            appId: '447972592264310',
            xfbml: true,
            version: 'v2.10'
        };

        this.facebookService.init(initParams);

        this.favouriteCountiesCookieKey = 'favouriteCounties';
    }

    public SharePerformers(comparisonType: string) {
        var url = 'http://statisticiromania.ro' + this.router.url + '?share=true&analysis=' + comparisonType + '&favouriteCounties=' + this.cookieService.get(this.favouriteCountiesCookieKey);

        this.Share(url);
    }

    public ShareCountyDetails(countyId1: number, countyId2: number, chapter: string, needToProcessAllYear: boolean) {
        var url = 'http://statisticiromania.ro' + this.router.url + '?share=true&countyId1=' + countyId1 + '&countyId2=' + countyId2 + '&chapter=' + chapter + '&needToProcessAllYear=' + needToProcessAllYear;

        this.Share(url);
    }

    public ShareStandings(chapter: string, year: number, yearFraction: number) {
        var url = 'http://statisticiromania.ro' + this.router.url + '?share=true&chapter=' + chapter + '&year=' + year + '&yearFraction=' + yearFraction + '&favouriteCounties=' + this.cookieService.get(this.favouriteCountiesCookieKey);

        this.Share(url);
    }

    Share(url: string) {
        let params: UIParams = {
            href: url,
            method: 'share'
        };

        this.facebookService.ui(params)
            .then((res: UIResponse) => console.log(res))
            .catch((e: any) => e != undefined ? console.log(e) : null);
    }
}