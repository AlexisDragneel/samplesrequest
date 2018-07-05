import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable'
import 'rxjs/add/observable/forkJoin'
import { Location } from '@angular/common';

import { SampleRequest } from '../../models/SampleRequest';
import { SampleRequestDetail } from '../../models/SampleRequestDetail';
import { SamplePurpose } from '../../models/SamplePurpose';
import { SamplePriority } from '../../models/SamplePriority';

import { SampleRequestService } from '../../services/samplerequest.service';
import { WorkflowService } from '../../services/workflow.service';
import { MessageService } from 'primeng/components/common/messageservice';

import { User } from "../../models/User";
import { WorkflowStep } from "../../models/WorkflowStep";
import { WorkflowUser } from "../../models/WorkflowUser";
import { RequestWorkflowStep } from "../../models/RequestWorkflowStep";
import { Client } from "../../models/Client";
import { Address } from "../../models/address";

@Component({
  selector: 'sr-request-form',
  templateUrl: './request-form.component.html',
  styleUrls: ['./request-form.component.css']
})
export class RequestFormComponent implements OnInit {

    request: SampleRequest = new SampleRequest();
    priorities: Array<SamplePriority> = new Array<SamplePriority>();
    purposes: Array<SamplePurpose> = new Array<SamplePurpose>();
    isLoading = false;
    isSubmiting = false;
    filterUsers: Array<User> = [];
    workflowSteps: Array<WorkflowStep> = [];
    filterWorkflowSteps: Array<WorkflowStep> = [];
    query: string = "";
    filterClients: Array<Client> = [];
    filteredAddreses: Array<Address> = [];
    selected_client: any;
    selected_address: any;
    msgs: any[] = [];
    errors: any[] = [];

    constructor(private requestService: SampleRequestService,
        private location: Location,
        private workflowService: WorkflowService,
        private messageService: MessageService) {
    }

    ngOnInit() {
        this.isLoading = true;
        this.request.address = null;

        Observable.forkJoin(this.requestService.GetAllPriorities(),
            this.requestService.GetAllPurposes(),
            this.workflowService.GetAll())
            .subscribe(response => {
                this.priorities = response[0].data;
                this.purposes = response[1].data;
                this.workflowSteps = response[2].data;
                this.request.sample_priority_id = this.priorities[0].id;
                this.request.sample_purpose_id = this.purposes[0].id;
                this.filterWorkflowSteps = this.workflowSteps.filter(step => !step.only_if_lab_test);
                this.isLoading = false;
            })
    }


    addNewDetail() {
        this.request.sample_request_details.push(new SampleRequestDetail());
    }

    removeDetail(i: number) {
        this.request.sample_request_details.splice(i, 1);
    }

    save() {

        this.isSubmiting = true;
        this.request.request_workflow_steps = this.bindWorkfloStepsIntoRequest(this.filterWorkflowSteps);

        this.buildAndBindAddressAndClient();
        
        this.requestService
            .Create(this.request)
           .subscribe(response => {
               this.isSubmiting = false;
               if (!response.success) {
                   console.log(response);
                   this.errors = response.messages.toString().split(',');
               } else {
                   this.msgs = [{ severity: 'info', summary: 'Confirmed', detail: "The request has been Created" }];
                   setTimeout(() => window.location.href = "/", 2000); 
               }
            }, error => {
                this.isSubmiting = false;
            });
    }

    private buildAndBindAddressAndClient(): void {
        if (this.request.require_shipping_info) {
            if (typeof this.selected_address === 'object')
                this.request.address = this.selected_address;
            else {
                if (this.request.address)
                    this.request.address.street_number = this.selected_address;
            }

            if (typeof this.selected_client === 'object') {
                if (!this.request.address)
                    this.request.address = new Address();
                this.request.address.client = this.selected_client;
            } else {
                if (this.request.address)
                    this.request.address.client.name = this.selected_client;
            }
        }

    }

    bindAddress(address: Address) {
        if (this.request.address)
            address.client = this.request.address.client;

        this.request.address = address;
    } 


    cancel() {
        this.location.back();
    }

    searchUser(event: any) {
        debugger;
        this.workflowService.GetAllUsersByName(event.query)
            .subscribe(response => {
                this.filterUsers = response.data;
            }, err => {
                console.log(err);
            });
    }


    addNewResponsibleIntoStep(event: User, step_index: number) {
        let newResponsible: WorkflowUser = new WorkflowUser();
        newResponsible.primary_responsible_id = event.uname;
        newResponsible.primary_responsible_name = event.name;
        this.workflowSteps[step_index].responsibles.push(newResponsible);
        this.query = "";
    }

    removeResponsibleFromStep(step: WorkflowStep, responsible_index: number) {
        step.responsibles.splice(responsible_index, 1);
    }

    bindWorkfloStepsIntoRequest(workflowSteps: Array<WorkflowStep>) {
        let array: Array<RequestWorkflowStep> = []
        workflowSteps.forEach(step => {
            step.responsibles.forEach(responsible => {
                array.push(new RequestWorkflowStep(step.name, step.order, responsible.primary_responsible_id, responsible.secondary_responsible_id, step.id));
            });
        });
        return array;
    }

    searchClient(event: any) {
        this.requestService.GetAllClientsByName(event.query)
            .subscribe(response => {
                this.filterClients = response.data;
            }, err => {
                console.log(err);
            })
    }



    searchAddress(event: any) {
        if (this.selected_client.id) {
            this.requestService.GetAllAddressesByStreet(this.selected_client.id, event.query)
                .subscribe(response => {
                        this.filteredAddreses = response.data;
                    },
                    err => {
                        console.log(err);
                    });
        } else {

            this.filterUsers = [];
        }
    }


    filterSteps() {
        if (this.request.require_test) {
            this.filterWorkflowSteps = this.workflowSteps;
        } else {
            this.filterWorkflowSteps = this.workflowSteps.filter(step => !step.only_if_lab_test);
        }
    }


}
