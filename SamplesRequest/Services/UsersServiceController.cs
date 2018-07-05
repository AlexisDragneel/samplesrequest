using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SamplesRequest.Models;
using SamplesRequest.Repositories;
using AutoMapper;
using SamplesRequest.Dtos;
using SamplesRequest.Models.Models.DAL.Repositories;
using SamplesRequest.Models.Models.Entities;
using SamplesRequest.Models.Models.ViewModels;

namespace SamplesRequest.Services
{
    [Route("api/[controller]/[action]")]
    public class UsersServiceController : Controller
    {
        private readonly IGenericRepository<User> _usersRepository;
        private readonly IMapper _mapper;

        public UsersServiceController(IGenericRepository<User> usersReposiotry, IMapper mapper)
        {
            _usersRepository = usersReposiotry;
            _usersRepository.ModelState = ModelState;
            _mapper = mapper;
        }


        [HttpGet]
        public ResultObject GetAllUsersByName(string query = "")
        {
            try
            {
                return new ResultObject
                {
                    success = true,
                    data = _mapper.Map<List<UserBaseDto>>(_usersRepository.List().Where(u => u.name.ToLower().Contains(query.ToLower()) || u.uname.ToLower().Contains(query.ToLower())))
                };
            }
            catch (Exception err)
            {
                return new ResultObject
                {
                    success = false,
                    messages = { err.Message }
                };
            }
        }


        [HttpPost]
        public ResultObject Create(UserBaseDto user)
        {
            if (ModelState.IsValid)
            {
                var data = _usersRepository.Save(_mapper.Map<User>(user));
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
    }
}
