import { Component, OnInit, NgZone } from '@angular/core';
import { Router } from '@angular/router';

import { UserService } from '../../services/user.service';
import { HttpService } from '../../services/http.service';
import { UserResponseModel } from '../../models/user.model';
import { LeagueService } from '../../services/league.service';
import { League } from '../../models/league.model';
import { Competition } from '../../models/competition.model';
import { CompetitionService } from '../../services/competition.service';
import { HistoryBlock } from '../../models/historyblock.model';

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
    userHistory: HistoryBlock[];
    userCompetitions: Competition[];

    selectedLeague: League;
    selectedCompetition: Competition;

    constructor(
        private router: Router,
        private userService: UserService,
        private leagueService: LeagueService,
        private competitionService: CompetitionService) 
    {
        this.userId = 0;

        this.user = new UserResponseModel();

        this.users = [];
        this.leagues = [];
        this.competitions = [];
        this.userHistory = [];
        this.userCompetitions = [];

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

    setAdmin(userId: number): any {
        this.userService.setAdmin(userId).subscribe((data: any) => {
            this.goToUsers();
        });
    };

    setStudent(userId: number): any {
        this.userService.setStudent(userId).subscribe((data: any) => {
            this.goToUsers();
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

    editCompetition(competition: Competition): any {
        this.selectedCompetition = competition;
    };

    saveCompetition(): any {
        var competitionPromise;

        if (this.selectedCompetition.Id == 0) {
            competitionPromise = this.competitionService.add(this.selectedCompetition);
        }
        else {
            competitionPromise = this.competitionService.edit(this.selectedCompetition);
        }

        competitionPromise.subscribe((data: any) => {
            if (data.error) {
                console.log(data.errorMessage);
            }
            else {
                this.selectedCompetition = new Competition();
                this.goToCompetitions();
            }
        });
    };

    deleteCompetition(competitionId: number): any {
        this.competitionService.delete(competitionId).subscribe((data: any) => {
            this.goToCompetitions();
        });
    };

    resetCompetition(): any {
        this.selectedCompetition = new Competition();
    };

    //profile
    goToProfile(): any {
        this.selectedTab = 'Profile';

        this.userService.getById(this.userId).subscribe((userData: any) => {
            this.user = userData;
        });

        this.userService.getHistory(this.userId).subscribe((userData: any) => {
            this.userHistory = userData;
        });

        this.userService.getCompetitions(this.userId).subscribe((userData: any) => {
            this.userCompetitions = userData;
        });
    };

    saveProfile(): any {
        this.userService.edit(this.user).subscribe((data: any) => {
            this.goToProfile();
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