
using Microsoft.EntityFrameworkCore;
using SamplesRequest.Models.Models.DAL.DBContext;
using SamplesRequest.Models.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SamplesRequest.Models.Models.DAL.Repositories
{
    public class SamplesRequestsRepository : GenericRepository<Entities.SampleRequest>, ISamplesRequestsRepository
    {
        public SamplesRequestsRepository(SamplesRequestDBContext context) : base(context)
        {
        }

        public int? SaveAndGetId(Entities.SampleRequest objToSave)
        {
            
            if (objToSave.address != null)
            {
                Context.Entry(objToSave.address).State = objToSave.address_id > 0 ? EntityState.Modified : EntityState.Added;
                Context.Entry(objToSave.address.client).State = objToSave.address.client_id > 0 ? EntityState.Modified : EntityState.Added;
            }

            if (Save(objToSave))
                return objToSave.id;

            return null;
        }

        public IEnumerable<SampleRequest> PagedList(IQueryable<SampleRequest> source, int page = 1, int itemsPerPage = 5)
        {
            return source.ToList().Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
        }

    }
}
