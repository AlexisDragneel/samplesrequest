import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map'
import { SampleRequest } from '../models/SampleRequest';
import { EndPoints } from './enpoints';

@Injectable()
export class SampleRequestService {


    constructor( @Inject(HttpClient) private http: HttpClient) {

    }

    GetAll(page: number, size: number, id_filter: string, project_filter: string = "",
        priority_filter: number = 0, client_filter: string = ""): Observable<any> {
        return this.http.get(EndPoints.SAMPLES_REQUEST_GET_URL, {
            params: new HttpParams().set('page', page.toString())
                .set('size', size.toString())
                .set('id_filter', id_filter)
                .set('project_filter', project_filter)
                .set('priority_filter', priority_filter.toString())
                .set('client_filter', client_filter)
        });
    }

    GetAllPurposes(): Observable<any> {
        return this.http.get(EndPoints.PURPOSES_URL);
    }

    GetAllPriorities(): Observable<any> {
        return this.http.get(EndPoints.PRIORITIES_URL);
    }

    GetById(id: number): Observable<any> {
        return this.http.get(EndPoints.SAMPLES_REQUEST_GET_BY_ID_URL, {
            params: new HttpParams().set('id', id.toString())
        });
    }

    Create(request: SampleRequest): Observable<any> {
        return this.http.post(EndPoints.SAMPLES_REQUEST_CREATE_URL, request);
    }

    GetAllClientsByName(query: string): Observable<any> {
        return this.http.get(EndPoints.CLIENTS_URL, {
            params: new HttpParams().set('query', query)
        });
    }

    GetAllAddressesByStreet(client_id: number, query: string): Observable<any> {
        return this.http.get(EndPoints.ADDRESSES_URL, {
            params: new HttpParams().set('client_id', client_id.toString()).set('query', query)
        });
    }
}