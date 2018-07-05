using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using SamplesRequest.Dtos;
using SamplesRequest.Models.Models.DAL.Repositories;
using SamplesRequest.Models.Models.Entities;
using SamplesRequest.Models.Models.ViewModels;

namespace SamplesRequest.Services
{
    [Route("api/[controller]/[action]")]
    public class SampleRequestServiceController : ControllerBase, ISampleRequestServiceController
    {
        private readonly ISamplesRequestsRepository _requestRepository;
        private readonly IGenericRepository<SamplePriority> _prioritiesRepository;
        private readonly IGenericRepository<SamplePurpose> _purposesRepository;
        private readonly IGenericRepository<Client> _clienteRepository;
        private readonly IGenericRepository<Address> _addresRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public SampleRequestServiceController(ISamplesRequestsRepository requestRepository, IMapper mapper,
            IGenericRepository<SamplePriority> prioritiesRepository, IGenericRepository<SamplePurpose> purposesRepository,
            IEmailService emailService, IGenericRepository<Client> clienteRepository, IGenericRepository<Address> addresRepository)
        {
            _requestRepository = requestRepository;
            _requestRepository.ModelState = ModelState;
            _mapper = mapper;
            _prioritiesRepository = prioritiesRepository;
            _prioritiesRepository.ModelState = ModelState;
            _purposesRepository = purposesRepository;
            _purposesRepository.ModelState = ModelState;
            _emailService = emailService;
            _clienteRepository = clienteRepository;
            _addresRepository = addresRepository;
        }

        [HttpGet]
        public ResultObject GetAll(int page = 1, int size = 5, int? id_filter = null, string project_filter = "",int priority_filter = 0, string client_filter = "")
        {
            var source = _requestRepository.List(includeProperties: "address,address.client,priority,request_workflow_steps");
            if (id_filter != null)source = source.Where(r => r.id.ToString().Contains(id_filter.ToString()));
            if (project_filter != "" && project_filter != null) source = source.Where(r => r.project_name.ToLower().Contains(project_filter.ToLower()));
            if (priority_filter != 0) source = source.Where(r => r.sample_priority_id == priority_filter);
            if (client_filter != "" && client_filter != null ) source = source.Where(r => r.address!= null && r.address.client.name.ToLower().Contains(client_filter.ToLower()));
            var data = _mapper.Map<List<SampleRequestWithStatusDto>>(_requestRepository.PagedList(source, page:page,itemsPerPage: size));
            return new ResultObject()
            {
                success = data != null ? true : false,
                data = new { size = source.Count(), list = data } ,
                messages = ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList(),
            };
            

        }

        [HttpGet]
        public ResultObject GetAllPurposes()
        {
            var data = _mapper.Map<List<SamplePurposeBaseDto>>(_purposesRepository.List());
            return new ResultObject()
            {
                success = data != null ? true : false,
                data = data,
                messages = ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList(),
            };
        }

        [HttpGet]
        public ResultObject GetAllPriorities()
        {
            var data = _mapper.Map<List<SamplePriorityBaseDto>>(_prioritiesRepository.List());
            return new ResultObject()
            {
                success = data != null ? true : false,
                data = data,
                messages = ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList()
            };
        }
        

        [HttpGet]
        public ResultObject GetById(int id)
        {
            var data = _mapper.Map<SampleRequestWithStatusDto>(_requestRepository.GetBy(r => r.id == id, "address,request_workflow_steps,sample_request_details"));
            return new ResultObject()
            {
                success = data != null ? true : false,
                data = data,
                messages = ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList()
            };
           
        }

        [HttpPost]
        public ResultObject Create([FromBody] SampleRequestDto request)
        {
            if (ModelState.IsValid) {
                var data = _requestRepository.SaveAndGetId(_mapper.Map<SampleRequest>(request));
                var response = new ResultObject()
                {
                    success = data != null ? true:false,
                    data = data,
                    messages = ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList()
                };

                if (response.success)
                    _emailService.SendNotificationResponsibleUser(data.Value);
                return response;
            }

            return new ResultObject()
            {
                success = false,
                messages = ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList()
            };
            
        }

        [HttpPut]
        public ResultObject Update([FromBody] SampleRequestDto request)
        {
            if (ModelState.IsValid)
            {
                var data = _requestRepository.Update(_mapper.Map<Models.Models.Entities.SampleRequest>(request));
                return new ResultObject()
                {
                    success = data,
                    data = data ,
                    messages = ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList()
                };
            }
            return new ResultObject()
            {
                success = false,
                messages = ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList()
            };
        }

        [HttpGet]
        public ResultObject GetAllClientsByName(string query = "")
        {
            try
            {
                return new ResultObject()
                {
                    success = true,
                    data = _mapper.Map<List<ClientBaseDto>>(_clienteRepository.List().Where(c => c.name.ToLower().Contains(query.ToLower())))
                };
            }
            catch (Exception err)
            {
                return new ResultObject()
                {
                    success = false,
                    messages = { err.Message }
                };
            }
        }

        [HttpGet]
        public ResultObject GetAllAddressesByStreet(int client_id,string query = "")
        {
            try
            {
                return new ResultObject()
                {
                    success = true,
                    data = _mapper.Map<List<AddressDto>>(_addresRepository.List(includeProperties:"client").Where(a => a.client_id == client_id && a.street_number.ToLower().Contains(query.ToLower())))
                };
            }catch(Exception err)
            {
                return new ResultObject()
                {
                    success = false,
                    messages = { err.Message }
                };
            }
        }

    }
}
