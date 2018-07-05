import { Client } from './Client';
import {SampleRequestDetail} from './SampleRequestDetail';
import { RequestWorkflowStep } from "./RequestWorkflowStep";
import { Address } from "./address";

export class SampleRequest {

    private _require_shipping_info: boolean = false;

    get require_shipping_info(): boolean {
        return this._require_shipping_info;
    }

    set require_shipping_info(value: boolean) {
        if (!value) {
            this.address = null;
        } else {
            this.address = new Address();
        }
        this._require_shipping_info = value;
    }

    constructor() {
        this.sample_request_details.push(new SampleRequestDetail());
        this.address = new Address();
    }

    id: number = 0;
    created_by: number = 0;
    created_at: Date = new Date();
    address: Address | null = null;
    sample_purpose_id: number ;
    sample_priority_id: number ;

    project_name: string = "";
    objective: string = "";
    comments: string = "";
    require_test: boolean = false;


    sample_request_details: Array<SampleRequestDetail> = [];
    request_workflow_steps: Array<RequestWorkflowStep> = [];
}
