import { WorkflowUser } from './WorkflowUser';

export class WorkflowStep {
    id: number;
    name: string;
    description: string;
    only_if_lab_test: boolean;
    order: number;

    responsibles: Array<WorkflowUser> = new Array<WorkflowUser>();
}