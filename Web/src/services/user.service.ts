import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { UserAuthorizationRequestModel, UserRegistrationRequestModel } from '../models/user.model';

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
}