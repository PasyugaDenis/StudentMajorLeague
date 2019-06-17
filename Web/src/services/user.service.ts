import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { UserAuthorizationRequestModel, UserRegistrationRequestModel, UserResponseModel } from '../models/user.model';

@Injectable()
export class UserService{
    baseController = "User/"

    constructor(private httpService: HttpService) { };

    authorization (user: UserAuthorizationRequestModel): any {
        return this.httpService.postData(this.baseController + 'Authorization', user);
    };

    register (user: UserRegistrationRequestModel): any {
        return this.httpService.postData(this.baseController + 'Registration', user);
    };

    getById (userId: number): any {
        return this.httpService.getData(this.baseController + userId);
    };

    getAll (): any {
        return this.httpService.getData(this.baseController);
    };

    getHistory (userId: number): any {
        return this.httpService.getData(this.baseController + "History/" + userId);
    };

    getCompetitions (userId: number): any {
        return this.httpService.getData(this.baseController + "Competitions/" + userId);
    };

    delete (userId: number): any {
        return this.httpService.getData(this.baseController + "Remove/" + userId);
    };

    edit (user: UserResponseModel): any {
        return this.httpService.postData(this.baseController + "Update", user);
    };

    setAdmin (userId: number): any {
        return this.httpService.getData(this.baseController + "SetAdmin/" + userId);
    };
    
    setStudent (userId: number): any {
        return this.httpService.getData(this.baseController + "SetStudent/" + userId);
    };
}