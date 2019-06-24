import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { TranslatePipe } from '../translate/translate.pipe';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';

const appRoutes: Routes = [
    { path: '', component: LoginComponent },
    { path: 'home', component: HomeComponent },
    { path: '**', redirectTo: '/' }
];

@NgModule({
    imports:      [ 
        BrowserModule,
        FormsModule, 
        HttpClientModule, 
        RouterModule.forRoot(appRoutes) 
    ],
    declarations: [ 
        AppComponent, 
        HomeComponent, 
        LoginComponent, 
        TranslatePipe 
    ],
    bootstrap:    [ 
        AppComponent 
    ]
})

export class AppModule { }