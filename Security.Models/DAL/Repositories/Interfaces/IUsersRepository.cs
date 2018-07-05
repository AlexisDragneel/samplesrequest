using Security.Models.DAL.GenericRepositories;
using Security.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Models.DAL.Repositories
{
    public interface IUsersRepository : IGenericRepository<User>
    {
    }
}
