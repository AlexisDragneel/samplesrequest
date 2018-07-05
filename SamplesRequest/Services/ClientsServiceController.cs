using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SamplesRequest.Models.Models.DAL.Repositories;
using SamplesRequest.Models.Models.Entities;
using AutoMapper;
using SamplesRequest.Models.Models.ViewModels;
using SamplesRequest.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SamplesRequest.Services
{
    [Route("api/[controller]/[action]")]
    public class ClientsServiceController : Controller
    {
        private readonly IGenericRepository<Client> _clientRepository;
        private readonly IMapper _mapper;

        public ClientsServiceController(IGenericRepository<Client> clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _clientRepository.ModelState = ModelState;
            _mapper = mapper;
        }

        [HttpGet]
        public ResultObject GetAll()
        {
            var data = _mapper.Map<List<ClientBaseDto>>(_clientRepository.List());
            return new ResultObject()
            {
                success = data != null,
                data = data,
                messages = ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage).ToList()
            };
        }
    }
}
