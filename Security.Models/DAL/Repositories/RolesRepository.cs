using Security.Models.DAL.GenericRepositories;
using Security.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Models.DAL.Repositories
{
    public class RolesRepository : GenericRepository<Role>, IRolesRepository
    {
        public RolesRepository( SecurityDbContext context) : base(context)
        {

        }
    }
}
