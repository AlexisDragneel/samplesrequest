using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SamplesRequest.Dtos;
using SamplesRequest.Models.Models.Entities;


namespace SamplesRequest.AutoMapperConfigs
{
    public class AppMapperProfile: Profile
    {
        public AppMapperProfile()
        {
            //Create maps here

            CreateMap<Models.Models.Entities.SampleRequest, SampleRequestBaseDto>().ReverseMap();
            CreateMap<Models.Models.Entities.SampleRequest, SampleRequestDto>().ReverseMap();
            CreateMap<Models.Models.Entities.SampleRequest, SampleRequestWithStatusDto>().ReverseMap();
            CreateMap<SampleRequestDetail, SampleRequestDetailBaseDto>().ReverseMap();
            CreateMap<Client, ClientBaseDto>().ReverseMap();
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<Address, AddressBaseDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<SamplePurpose, SamplePurposeBaseDto>().ReverseMap();
            CreateMap<SamplePriority, SamplePriorityBaseDto>().ReverseMap();
            CreateMap<Catalog, CatalogDto>().ReverseMap();
            CreateMap<User, UserBaseDto>().ReverseMap();
            CreateMap<WorkflowStep, WorkflowStepBaseDto>().ReverseMap();
            CreateMap<WorkflowStep, WorkflowStepWithResponsiblesDto>().ReverseMap();
            CreateMap<WorkflowUser, WorkflowUserBaseDto>().ReverseMap();
            CreateMap<RequestWorkflowStep, RequestWorkflowStepBaseDto>().ReverseMap();
        }

    }
}
