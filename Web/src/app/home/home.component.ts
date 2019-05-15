import { Component, OnInit, NgZone } from '@angular/core';
import { Router } from '@angular/router';

import { UserService } from '../../services/user.service';
import { HttpService } from '../../services/http.service';
import { UserResponseModel } from '../../models/user.model';
import { LeagueService } from '../../services/league.service';
import { League } from '../../models/league.model';

@Component({
    selector: 'home-app',
    templateUrl: './home.component.html',
    providers: [
        HttpService,
        UserService,
        LeagueService
    ]
})

export class HomeComponent implements OnInit { 
    userId: number;
    selectedTab: string;

    readUsers: boolean;
    readLeagues: boolean;
    readCompetitions: boolean;
    writeUsers: boolean;
    writeLeagues: boolean;
    writeCompetitions: boolean;

    user: UserResponseModel;

    users: UserResponseModel[];
    leagues: League[];

    constructor(
        private router: Router,
        private userService: UserService,
        private leagueService: LeagueService) 
    {
        this.userId = 0;

        this.users = [];
        this.leagues = [];
    };

    ngOnInit(): any {
        this.userId = parseInt(localStorage.getItem('userId'));
        
        this.setClaims();
        this.goToUsers();
    };

    //navigation
    goToUsers(): any {
        this.selectedTab = 'Users';

        this.userService.getAll().subscribe((data: any) => {
            if (data.error) {
                console.log(data.errorMessage);
            }
            else {
                this.users = data;
            }
        });
    };

    goToLeagues(): any {
        this.selectedTab = 'Leagues';

        this.leagueService.getAll().subscribe((data: any) => {
            if (data.error) {
                console.log(data.errorMessage);
            }
            else {
                this.leagues = data;
                console.log(this.leagues);

            }
        });
    };

    goToCompetitions(): any {
        this.selectedTab = 'Competitions';

    };

    goToProfile(): any {
        this.selectedTab = 'Profile';

        this.userService.getById(this.userId).subscribe((userData: any) => {
            this.user = userData;
        });
    };

    exit(): any {
        localStorage.removeItem('app_token');
        localStorage.removeItem('userId');
        localStorage.removeItem('role');

        this.router.navigate(['']);
    };

    //data control

    //private
    private setClaims(): any {
        var role = localStorage.getItem('role');

        switch (role.toLowerCase()) {
            case 'admin':
                this.readUsers = true;
                this.readLeagues = true;
                this.readCompetitions = true;
                this.writeUsers = true;
                this.writeLeagues = true;
                this.writeCompetitions = true;
                break;

            case 'student':
                this.readUsers = true;
                this.readLeagues = true;
                this.readCompetitions = true;
                this.writeUsers = false;
                this.writeLeagues = false;
                this.writeCompetitions = false;
                break;
        };
    };
}