using Microsoft.AspNetCore.Mvc;
using SamplesRequest.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SamplesRequest.Models.Models.ViewModels;

namespace SamplesRequest.Services
{
    public interface ISampleRequestServiceController
    {
        ResultObject GetAll(int page = 1, int size = 5, int? id_filter = null, string project_filter = "", int priority_filter = 0, string client_filter = "");
        ResultObject GetById(int id);
        ResultObject Create([FromBody] SampleRequestDto request);
        ResultObject Update([FromBody] SampleRequestDto request);
    }
}
