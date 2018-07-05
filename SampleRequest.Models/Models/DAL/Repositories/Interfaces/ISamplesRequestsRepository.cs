using SamplesRequest.Models.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamplesRequest.Models.Models.DAL.Repositories
{
    public interface ISamplesRequestsRepository : IGenericRepository<Entities.SampleRequest>
    {
        int? SaveAndGetId(Entities.SampleRequest objToSave);
        IEnumerable<SampleRequest> PagedList(IQueryable<SampleRequest> source, int page = 1, int itemsPerPage = 5);
    }
}
