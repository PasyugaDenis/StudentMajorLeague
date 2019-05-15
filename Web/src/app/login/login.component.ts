import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { HttpService } from '../../services/http.service';
import { Router } from '@angular/router';
import { UserRegistrationRequestModel, UserAuthorizationRequestModel } from '../../models/user.model';
import { League } from '../../models/league.model';
import { LeagueService } from '../../services/league.service';
  
@Component({
    selector: 'login-app',
    templateUrl: './login.component.html',
    providers: [
        HttpService,
        UserService, 
        LeagueService
    ]
})
export class LoginComponent implements OnInit { 
    isRegistration: boolean;

    registrationResponse: UserRegistrationRequestModel;
    authorizationResponse: UserAuthorizationRequestModel;

    //selectedLeague: League;
    leagues: League[];

    constructor(
        private router: Router,
        private userService: UserService,
        private leagueService: LeagueService) 
    {  
        this.isRegistration = false;

        this.registrationResponse = new UserRegistrationRequestModel();
        this.authorizationResponse = new UserAuthorizationRequestModel();

        this.leagues = [];
    }

    ngOnInit() {
        var token = localStorage.getItem('app_token');
        var userId = localStorage.getItem('userId');
        var role = localStorage.getItem('role');

        if (token != null && userId != null) {
            this.goToMain(token, userId, role);
        }

        this.leagueService.getAll().subscribe((data: any) => {
            if (data.error) {
                console.log(data.errorMessage);
            }
            else {
                this.leagues = data;
            }
        });
    };

    authorization(): any {
        this.userService.authorization(this.authorizationResponse).subscribe((data: any) => { 
            if (data.error) {
                console.log(data.errorMessage);
                alert(data.errorMessage);
            }
            else {
                this.goToMain(data.token, data.userId, data.role);
            }
        });
    };

    registration(): any {
        this.createUser();
    };

    private createUser(): any {
        this.userService.register(this.registrationResponse).subscribe((userData: any) => { 
            if (userData.error) {
                console.log(userData.errorMessage);
            }
            else {
                this.goToMain(userData.token, userData.userId, userData.role);
            }
        });
    };

    private goToMain(token: string, userId: string, role: string): any {
        localStorage.setItem('app_token', token);
        localStorage.setItem('userId', userId);
        localStorage.setItem('role', role);

        this.router.navigate(['home']);
    };
}