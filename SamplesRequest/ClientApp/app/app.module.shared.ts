import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AccordionModule } from 'primeng/primeng';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { SliderModule } from 'primeng/slider';
import { GrowlModule } from 'primeng/growl';
import { TableModule } from 'primeng/table';

import { AppComponent } from './components/app/app.component';
import { RequestFormComponent } from './components/request-form/request-form.component';
import { WorkflowFormComponent } from './components/workflow-form/workflow-form.component';
import { ReviewAndApproveComponent } from './components/reviewandaprove-form/reviewandaprove-form.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';

import { SampleRequestService } from './services/samplerequest.service';
import { WorkflowService } from './services/workflow.service';
import { ClientService } from './services/client.service';
import { ConfirmationService } from 'primeng/api';
import { MessageService } from 'primeng/components/common/messageservice';

@NgModule({
    declarations: [
        AppComponent,
        RequestFormComponent,
        WorkflowFormComponent,
        ReviewAndApproveComponent,
        DashboardComponent
    ],
    imports: [
        CommonModule,
        HttpClientModule,
        FormsModule,
        AccordionModule,
        AutoCompleteModule,
        ConfirmDialogModule,
        GrowlModule,
        TableModule,
        SliderModule,
        RouterModule.forRoot([
            //{ path: '', redirectTo: 'SampleRequests', pathMatch: 'full' },
            { path: '', component: DashboardComponent },
            { path: 'SampleRequests/Create', component: RequestFormComponent },
            { path: 'WorkflowSteps', component: WorkflowFormComponent },
            { path: 'SampleRequests/ReviewAndApprove/:id', component: ReviewAndApproveComponent },
            { path: '**', redirectTo: '' }
        ])
    ],

    providers: [
        SampleRequestService,
        WorkflowService,
        ConfirmationService,
        MessageService,
        ClientService
    ]
})
export class AppModuleShared {
}
