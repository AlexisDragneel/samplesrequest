import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/Observable/forkJoin';
import * as _ from 'underscore';
import { Location } from '@angular/common';


import { SampleRequestService } from '../../services/samplerequest.service';
import { SamplePriority } from "../../models/SamplePriority";
import { SamplePurpose } from "../../models/SamplePurpose";
import { SampleRequest } from "../../models/SampleRequest";
import { RequestWorkflowStep } from "../../models/RequestWorkflowStep";

@Component({
    selector: 'sr-reviewandaprove-form.component',
    templateUrl: './reviewandaprove-form.component.html',
    styleUrls: ['./reviewandaprove-form.component.css']
})
export class ReviewAndApproveComponent implements OnInit {
    request: SampleRequest = new SampleRequest();
    params: any = {};
    priorities: Array<SamplePriority> = new Array<SamplePriority>();
    purposes: Array<SamplePurpose> = new Array<SamplePurpose>();
    requestSteps: Array<any> = new Array<any>();
    isLoading = false;

    constructor(private activedRoute: ActivatedRoute,
        private requestService: SampleRequestService,
        private location: Location) {
    }

    ngOnInit() {
        this.isLoading = true;
        this.activedRoute.params.subscribe((params: Params) => {
            this.params = params;
        });

        Observable.forkJoin(this.requestService.GetAllPurposes(),
            this.requestService.GetAllPriorities(),
            this.requestService.GetById(this.params.id))
            .subscribe(response => {
                this.purposes = response[0].data;
                this.priorities = response[1].data;
                this.request = response[2].data;
                this.requestSteps = _.toArray(_.groupBy(this.request.request_workflow_steps, step => step.order_workflow_step));
                this.isLoading = false;
            })
    }

    Back() {
        this.location.back();
    }


}