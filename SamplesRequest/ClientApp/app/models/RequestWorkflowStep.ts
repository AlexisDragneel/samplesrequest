
export class RequestWorkflowStep {
    constructor(step_name:string,step_order:number,uname:string, uname2:string, step_id:number) {
        this.id = 0;
        this.primary_responsible_id = uname;
        this.secondary_responsible_id = uname2;
        this.order_workflow_step = step_order;
        this.workflow_step_name = step_name;
        this.workflow_step_id = step_id;
    }

    id: number;
    primary_responsible_id: string;
    secondary_responsible_id: string;
    order_workflow_step: number;
    workflow_step_name: string;
    workflow_step_id: number;
    approved_by: string;
    approved_date: Date;

}