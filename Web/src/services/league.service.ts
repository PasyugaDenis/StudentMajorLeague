import { Injectable } from '@angular/core';
import { HttpService } from './http.service';

@Injectable()
export class LeagueService{
    baseController = "League/"
    
    constructor(private httpService: HttpService) { };

    getAll (): any {
        return this.httpService.getData(this.baseController)
    };
}