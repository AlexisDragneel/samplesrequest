import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { EndPoints } from './enpoints';


@Injectable()
export class ClientService {

    constructor( @Inject(HttpClient) private http: HttpClient) { }

    GetAll(): Observable<any> {
        return this.http.get(EndPoints.CLIENT_GET_URL);
    }
}