import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Competition } from '../models/competition.model';

@Injectable()
export class CompetitionService{
    baseController = "Competition/"
    
    constructor(private httpService: HttpService) { };

    getAll (): any {
        return this.httpService.getData(this.baseController)
    };

    add (competition: Competition): any {
        return this.httpService.postData(this.baseController + "Add", competition);
    };

    edit (competition: Competition): any {
        return this.httpService.postData(this.baseController + "Update", competition);
    };

    delete (competitionId: number): any {
        return this.httpService.getData(this.baseController + "Remove/" + competitionId);
    };
}