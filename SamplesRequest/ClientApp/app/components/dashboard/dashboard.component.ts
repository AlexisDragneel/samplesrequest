import { Component, OnInit } from '@angular/core';
import { SampleRequest } from "../../models/SampleRequest";
import { Observable } from 'rxjs/Observable'
import 'rxjs/add/observable/forkJoin'
import { SampleRequestService } from '../../services/samplerequest.service';
import { ClientService } from '../../services/client.service';
import { LazyLoadEvent } from "primeng/components/common/api";
import { SamplePriority } from "../../models/SamplePriority";
import { WorkflowStep } from "../../models/WorkflowStep";
import { Client } from "../../models/Client";


@Component({
    selector: 'sr-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

    requests: Array<SampleRequest> = new Array<SampleRequest>();
    loading: boolean;
    rowsPerPage: number[] = [5, 10, 20, 50, 100];
    totalRecords: number;
    id_filter: string = "";
    project_filter: string = "";
    client_filter: string = "";
    priority_filter: number = 0;
    priorities: Array<SamplePriority> = new Array<SamplePriority>();
    clients: Array<Client> = new Array<Client>();

    constructor(private requestService: SampleRequestService, private clientService: ClientService) {
        this.loading = true;
    }

    ngOnInit() {
        Observable.forkJoin(this.requestService.GetAllPriorities(), this.clientService.GetAll())
            .subscribe(response => {
                this.priorities = response[0].data;
                this.clients = response[1].data;
            })
    }

    loadRequestLazy(event: LazyLoadEvent) {
        let page = 1;
        let rows = event.rows != undefined ? event.rows : 5;
        console.log(event);
        if (event && event.first && event.rows) {
            page = (event.first / event.rows) + 1;
        }
        this.loading = true
        this.requestService
            .GetAll(page, rows, this.id_filter, this.project_filter, this.priority_filter, this.client_filter)
            .subscribe(response => {
                this.requests = response.data.list;
                this.totalRecords = response.data.size;
                this.loading = false;
            }, err => {
            })
    }

}