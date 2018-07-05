using Microsoft.AspNetCore.Mvc;
using SamplesRequest.Dtos;
using SamplesRequest.Models.Models.ViewModels;

namespace SamplesRequest.Services
{
    public interface IWorkflowServiceController
    {
        ResultObject Create([FromBody] WorkflowStepWithResponsiblesDto step);
        ResultObject Delete(int id);
        ResultObject DeleteUserById(int id);
        ResultObject GetAll();
        ResultObject Update([FromBody] WorkflowStepWithResponsiblesDto step);
    }
}