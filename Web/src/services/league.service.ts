import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { League } from '../models/league.model';

@Injectable()
export class LeagueService{
    baseController = "League/"
    
    constructor(private httpService: HttpService) { };

    getAll (): any {
        return this.httpService.getData(this.baseController)
    };

    add (league: League): any {
        return this.httpService.postData(this.baseController + "Add", league);
    };

    edit (league: League): any {
        return this.httpService.postData(this.baseController + "Update", league);
    };

    delete (leagueId: number): any {
        return this.httpService.getData(this.baseController + "Remove/" + leagueId);
    };
}