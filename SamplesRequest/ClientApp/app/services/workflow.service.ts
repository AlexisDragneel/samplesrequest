import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { EndPoints } from './enpoints';

import { WorkflowStep } from '../models/WorkflowStep';

@Injectable()
export class WorkflowService {

    constructor( @Inject(HttpClient) private http: HttpClient) {

    }

    GetAll(): Observable<any> {
        return this.http.get(EndPoints.WORKFLOW_STEPS_GET_URL);
    }

    GetAllUsersByName(query: string): Observable<any> {
        return this.http.get(EndPoints.USERS_BY_NAME, {
            params: new HttpParams().set('query', query)
        });
    }

    Create(step: WorkflowStep): Observable<any> {
        return this.http.post(EndPoints.WORKFLOW_STEPS_CREATE_URL, step);
    }

    Update(step: WorkflowStep): Observable<any> {
        return this.http.put(EndPoints.WORKFLOW_STEPS_UPDATE_URL, step);
    }

    MoveStep(step: WorkflowStep, type: number): Observable<any> {
        return this.http.put(EndPoints.WORKFLOW_STEPS_MOVE_STEP_URL, step, {
            params: new HttpParams().set('type', type.toString())
        });
    }


    Delete(id: number): Observable<any> {
        return this.http.delete(EndPoints.WORKFLOW_STEPS_DELETE_URL, {
            params: new HttpParams().set('id', id.toString())
        });
    }

    DeleteUserById(id: number): Observable<any> {
        return this.http.delete(EndPoints.DELETE_USER_URL, {
            params: new HttpParams().set('id', id.toString())
        });
    }

}