import { Component, OnInit } from '@angular/core';

import { WorkflowStep } from '../../models/WorkflowStep';
import { User } from '../../models/User';

import { WorkflowService } from '../../services/workflow.service';
import { WorkflowUser } from "../../models/WorkflowUser";
import { ConfirmationService } from 'primeng/api';
import { MessageService } from 'primeng/components/common/messageservice';

@Component({
    selector: 'sr-workflow-form',
    templateUrl: './workflow-form.component.html',
    styleUrls: ['./workflow-form.component.css'
    ]
})
export class WorkflowFormComponent implements OnInit {
    steps: Array<WorkflowStep> = [];
    filterUsers: Array<User> = [];
    query: string = "";
    isSubmiting: boolean = false;
    isLoading: boolean = false;
    msgs: any[] = [];

    constructor(private workflowService: WorkflowService,
        private confirmationService: ConfirmationService,
        private messageService: MessageService) { }

    ngOnInit() {
        this.getSteps();
    }


    getSteps() {
        this.isLoading = true;
        this.workflowService.GetAll().subscribe(response => {
            this.steps = response.data;
            if (!this.steps.length) this.steps.push(new WorkflowStep());
            this.isLoading = false;
        });
    }

    addNewStep() {
        this.steps.push(new WorkflowStep())
    }


    searchUser(event: any) {
        this.workflowService.GetAllUsersByName(event.query).subscribe(response => {
            this.filterUsers = response.data;
        }, err => {
            console.log(err);
        });

    }

    bindPrimaryResponsible(user: User, workflowUser: WorkflowUser) {
        workflowUser.primary_responsible_id = user.uname;
        workflowUser.primary_responsible_name = user.name;
    }

    bindSecundaryResponsible(user: User, workflowUser: WorkflowUser) {
        workflowUser.secondary_responsible_id = user.uname;
        workflowUser.secondary_responsible_name = user.name;
    }

    addNewWorkflowUser(step: WorkflowStep) {
        step.responsibles.push(new WorkflowUser());
    }



    save(step: WorkflowStep) {
        this.isSubmiting = true;
        step.order = this.steps.indexOf(step);
        if (step.id) {
            this.workflowService.Update(step).subscribe(response => {
                this.msgs = [{ severity: 'info', summary: 'Confirmed', detail: "The step has been Updated" }];
                this.isSubmiting = false;
            }, err => {
                console.log(err);
                this.isSubmiting = false;
            });

        } else {
            this.workflowService.Create(step).subscribe(response => {
                console.log(response);
                let i = 0;
                step.id = response.data.id;
                step.responsibles.forEach(responsible => {
                    responsible.id = response.data.responsibles[i].id;
                    i++;
                });
                this.msgs = [{ severity: 'info', summary: 'Confirmed', detail: "The step has been Created" }]
                this.isSubmiting = false;
            }, err => {
                console.log(err);
                this.isSubmiting = false;
            });
        }



    }

    confirmBeforeDelete(step: WorkflowStep) {
        if (step.id) {
            this.confirmationService.confirm({
                message: 'Do you want to delete this item ?',
                header: 'Delete workflow step',
                icon: 'fa fa-trash',
                accept: () => {
                    this.msgs = [{ severity: 'info', summary: 'Confirmed', detail: "The step has been delete" }]
                    this.workflowService.Delete(step.id).subscribe(response => {
                        this.msgs = [{ severity: 'info', summary: 'Deleted', detail: "the step has been deleted" }]
                    });
                    this.removeStep(this.steps.indexOf(step));
                },
                reject: () => {
                    this.msgs = [{ severity: 'info', summary: 'Reject', details: "canceled" }]
                }
            })
        } else {
            this.removeStep(this.steps.indexOf(step));
        }

    }

    DeleteUserById(step: WorkflowStep, responsible: WorkflowUser) {
        if (responsible.id) {
            this.workflowService.DeleteUserById(responsible.id).subscribe(response => {
                this.removeUserfromStep(step, step.responsibles.indexOf(responsible));
                this.msgs = [{ severity: 'info', summary: 'Deleted', detail: "The user has been Removed" }];
            });
        } else {
            this.removeUserfromStep(step, step.responsibles.indexOf(responsible))
        }

    }

    removeUserfromStep(step: WorkflowStep, user_index: number) {
       step.responsibles.splice(user_index, 1);
    }

    removeStep(i: number) {
        this.steps.splice(i, 1);
    }

    moveStep(step: WorkflowStep, type: number) {
        this.workflowService.MoveStep(step, type)
            .subscribe(response => {
                if (response.data) {
                    if (type == 1) {
                        let current_order = step.order;
                        this.steps[step.order - 1].order = current_order;
                        step.order--;
                        this.steps = this.steps.sort((s: WorkflowStep, s2: WorkflowStep) => s.order - s2.order);
                    } else {
                        let current_order = step.order;
                        this.steps[step.order + 1].order = current_order;
                        step.order++;
                        this.steps = this.steps.sort((s: WorkflowStep, s2: WorkflowStep) => s.order - s2.order);
                    }
                } else {
                    this.msgs = [{ severity: 'info', summary: 'Info', detail: "This is the step with most priority" }];
                }
            })
    }

 }