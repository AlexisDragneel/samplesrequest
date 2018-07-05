using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SamplesRequest.Repositories;
using AutoMapper;
using SamplesRequest.Dtos;
using SamplesRequest.Models.Models.DAL.Repositories;
using SamplesRequest.Models.Models.Entities;
using SamplesRequest.Models.Models.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SamplesRequest.Services
{
    [Route("api/[controller]/[action]")]
    public class WorkflowServiceController : Controller, IWorkflowServiceController
    {

        private readonly IMapper _mapper;
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IGenericRepository<WorkflowUser> _workflowUserRepository;

        public WorkflowServiceController(IMapper mapper, 
                                        IWorkflowRepository workflowRepository,
                                        IGenericRepository<WorkflowUser> workflowUserRepository)
        {
            _mapper = mapper;
            _workflowRepository = workflowRepository;
            _workflowRepository.ModelState = ModelState;
            _workflowUserRepository = workflowUserRepository;
            _workflowUserRepository.ModelState = ModelState;
        }

        // GET: api/values
        [HttpGet]
        public ResultObject GetAll()
        {
            var data = _mapper.Map<List<WorkflowStepWithResponsiblesDto>>(_workflowRepository.List(orderBy: e => e.OrderBy(s => s.order), includeProperties:"responsibles"));
            return new ResultObject()
            {
                success = data != null ? true : false,
                data = data,
                messages = ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList()
            };
        }


        // POST api/values
        [HttpPost]
        public ResultObject Create([FromBody]WorkflowStepWithResponsiblesDto step)
        {
            if (ModelState.IsValid)
            {
                var data = _workflowRepository.SaveAndReturnStep(_mapper.Map<WorkflowStep>(step));
                return new ResultObject()
                {
                    success = data != null ? true : false,
                    data = _mapper.Map<WorkflowStepWithResponsiblesDto>(data),
                    messages = ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList()
                };
            }
                return new ResultObject()
                {
                    success = false,
                    messages = ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList()
                };
            
        }

        [HttpPut]
        public ResultObject Update([FromBody]WorkflowStepWithResponsiblesDto step)
        {
                if (ModelState.IsValid)
                {
                var data = _workflowRepository.Update(_mapper.Map<WorkflowStep>(step));
                    return new ResultObject()
                    {
                        success = data,
                        data = data,
                        messages = ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList()
                    };
                }
                    return new ResultObject()
                    {
                        success = false,
                        messages = ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList()
                    };
        }

        [HttpDelete]
        public ResultObject Delete(int id)
        {
            var data = _workflowRepository.DeleteBy(step => step.id == id);
            return new ResultObject()
            {
                success = data,
                data = data,
                messages = ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList()
            };
        }

        [HttpPut]
        public ResultObject MoveStep([FromBody] WorkflowStepBaseDto step,int type = 1)
        {
            var data = _workflowRepository.Update(_mapper.Map<WorkflowStep>(step), type);
            return new ResultObject()
            {
                success = data,
                data = data,
                messages = ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList()
            };
        }




        [HttpDelete]
        public ResultObject DeleteUserById(int id)
        {
            var data = _workflowUserRepository.DeleteBy(step => step.id == id);
            return new ResultObject()
            {
                success = data,
                data = data,
                messages = ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList()
            };
        }
    }
}
