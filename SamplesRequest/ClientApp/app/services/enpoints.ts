export class EndPoints {
    static SAMPLES_REQUEST_ENDPOINT: string = "api/samplerequestservice/";
    static SAMPLES_REQUEST_GET_URL: string = EndPoints.SAMPLES_REQUEST_ENDPOINT + "GetAll";
    static SAMPLES_REQUEST_GET_BY_ID_URL: string = EndPoints.SAMPLES_REQUEST_ENDPOINT + "GetById";
    static SAMPLES_REQUEST_CREATE_URL: string = EndPoints.SAMPLES_REQUEST_ENDPOINT + "Create";
    static SAMPLES_REQUEST_UPDATE_URL: string = EndPoints.SAMPLES_REQUEST_ENDPOINT + "Update";
    static PURPOSES_URL: string = EndPoints.SAMPLES_REQUEST_ENDPOINT + "GetAllPurposes";
    static PRIORITIES_URL: string = EndPoints.SAMPLES_REQUEST_ENDPOINT + "GetAllPriorities";
    static CLIENTS_URL: string = EndPoints.SAMPLES_REQUEST_ENDPOINT + "GetAllClientsByName";
    static ADDRESSES_URL: string = EndPoints.SAMPLES_REQUEST_ENDPOINT + "GetAllAddressesByStreet";

    static WORKFLOW_STEPS_ENDPOINT: string = "api/workflowservice/";
    static WORKFLOW_STEPS_GET_URL: string = EndPoints.WORKFLOW_STEPS_ENDPOINT + "GetAll";
    static WORKFLOW_STEPS_CREATE_URL: string = EndPoints.WORKFLOW_STEPS_ENDPOINT + "Create";
    static WORKFLOW_STEPS_UPDATE_URL: string = EndPoints.WORKFLOW_STEPS_ENDPOINT + "Update";
    static WORKFLOW_STEPS_DELETE_URL: string = EndPoints.WORKFLOW_STEPS_ENDPOINT + "Delete";
    static WORKFLOW_STEPS_MOVE_STEP_URL: string = EndPoints.WORKFLOW_STEPS_ENDPOINT + "MoveStep";
    static DELETE_USER_URL: string = EndPoints.WORKFLOW_STEPS_ENDPOINT + "deleteUserById";

    static USERS_ENDPOINT: string = "api/usersservice/";
    static USERS_BY_NAME: string = EndPoints.USERS_ENDPOINT + "getAllUsersByName";

    static CLIENT_ENDPOIT: string = "api/clientsservice/";
    static CLIENT_GET_URL: string = EndPoints.CLIENT_ENDPOIT + "GetAll";
}