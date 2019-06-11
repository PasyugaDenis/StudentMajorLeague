import { Component, OnInit, NgZone } from '@angular/core';
import { Router } from '@angular/router';

import { UserService } from '../../services/user.service';
import { HttpService } from '../../services/http.service';
import { UserResponseModel } from '../../models/user.model';
import { LeagueService } from '../../services/league.service';
import { League } from '../../models/league.model';
import { Competition } from '../../models/competition.model';
import { CompetitionService } from '../../services/competition.service';

@Component({
    selector: 'home-app',
    templateUrl: './home.component.html',
    providers: [
        HttpService,
        UserService,
        LeagueService,
        CompetitionService
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
    competitions: Competition[];

    selectedLeague: League;
    selectedCompetition: Competition;

    constructor(
        private router: Router,
        private userService: UserService,
        private leagueService: LeagueService,
        private competitionService: CompetitionService) 
    {
        this.userId = 0;

        this.users = [];
        this.leagues = [];

        this.selectedLeague = new League();
        this.selectedCompetition = new Competition();
    };

    ngOnInit(): any {
        this.userId = parseInt(localStorage.getItem('userId'));
        
        this.setClaims();
        this.goToUsers();
    };

    //users
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

    deleteUser(userId: number): any {
        this.userService.delete(userId).subscribe((data: any) => {
            if (data.error) {
                console.log(data.errorMessage);
            }
            else {
                this.goToUsers();
            }
        });
    };

    //leagues   
    goToLeagues(): any {
        this.selectedTab = 'Leagues';

        this.leagueService.getAll().subscribe((data: any) => {
            if (data.error) {
                console.log(data.errorMessage);
            }
            else {
                this.leagues = data;
            }
        });
    };

    editLeague(league: League): any {
        this.selectedLeague = league;
    };

    saveLeague(): any {
        var leaguePromise;

        if (this.selectedLeague.Id == 0) {
            this.selectedLeague.MainLeagueId = null;

            leaguePromise = this.leagueService.add(this.selectedLeague);
        }
        else {
            leaguePromise = this.leagueService.edit(this.selectedLeague);
        }

        leaguePromise.subscribe((data: any) => {
            if (data.error) {
                console.log(data.errorMessage);
            }
            else {
                this.selectedLeague = new League();
                this.goToLeagues();
            }
        });
    };

    deleteLeague(leagueId: number): any {
        this.leagueService.delete(leagueId).subscribe((data: any) => {
            this.goToLeagues();
        });
    };

    resetLeague(): any {
        this.selectedLeague = new League();
    };

    //competitions
    goToCompetitions(): any {
        this.selectedTab = 'Competitions';

        this.competitionService.getAll().subscribe((data: any) => {
            if (data.error) {
                console.log(data.errorMessage);
            }
            else {
                this.competitions = data;
            }
        });
    };

    deleteCompetition(competitionId: number): any {
        this.competitionService.delete(competitionId).subscribe((data: any) => {
            this.goToCompetitions();
        });
    };

    //profile
    goToProfile(): any {
        this.selectedTab = 'Profile';

        this.userService.getById(this.userId).subscribe((userData: any) => {
            this.user = userData;
        });
    };

    exit(): any {
        localStorage.removeItem('app_token');
        localStorage.removeItem('userId');
        localStorage.removeItem('roleId');

        this.router.navigate(['']);
    };

    //private
    private setClaims(): any {
        var roleId = localStorage.getItem('roleId');

        switch (roleId) {
            case '1': //admin
                this.readUsers = true;
                this.readLeagues = true;
                this.readCompetitions = true;
                this.writeUsers = true;
                this.writeLeagues = true;
                this.writeCompetitions = true;
                break;

            case '2': //student
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