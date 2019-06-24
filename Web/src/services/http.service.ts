import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';

@Injectable()
export class HttpService{
    basePath: string;
    token: string;

    constructor(private http: HttpClient) { 
        this.basePath = 'https://studentmajorleaguebeta.azurewebsites.net/api/';
        this.token = localStorage.getItem('token');     
    };
      
    postData(path: string, model: any) {      
        var headers = this.setHeaders();

        return this.http.post(this.basePath + path, model, { headers: headers });
    };

    getData(path: string) {
        var headers = this.setHeaders();

        return this.http.get(this.basePath + path, { headers: headers }); 
    };

    private setHeaders(): HttpHeaders {
        var headers = new HttpHeaders();

        headers.append('Content-Type', 'application/json');
        headers.append('authentication', this.token);

        return headers;
    };
}