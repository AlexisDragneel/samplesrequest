using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SamplesRequest.Models.Models.DAL.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        DbContext BaseContext();
        ModelStateDictionary ModelState { get; set; }

        IEnumerable<TEntity> PagedList(Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy, string includeProperties = "",
            Expression<Func<TEntity, Boolean>> filter = null, int page = 1, int itemsPerPage = 10);

        IEnumerable<TDerivedType> PagedList<TDerivedType>(Func<IQueryable<TDerivedType>, 
            IOrderedQueryable<TDerivedType>> orderBy, string includeProperties = "",
            Expression<Func<TDerivedType, Boolean>> filter = null, int page = 1, int itemsPerPage = 10) where TDerivedType : class;

        IQueryable<TEntity> List(Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null,
            Expression<Func<TEntity, Boolean>> filter = null, string includeProperties = "");

        IEnumerable<TDerivedType> List<TDerivedType>(
            Func<IQueryable<TDerivedType>, IOrderedQueryable<TDerivedType>> orderBy = null,
            Expression<Func<TDerivedType, Boolean>> filter = null) where TDerivedType : class;

        TEntity GetBy(Expression<Func<TEntity, Boolean>> filter, string includeProperties = "");

        TDerivedType GetBy<TDerivedType>(Expression<Func<TDerivedType, Boolean>> filter, string includeProperties = "")
            where TDerivedType : class;

        Boolean Save(TEntity objToSave);

        Boolean Update(TEntity objToUpdate);

        Boolean DeleteBy(Expression<Func<TEntity, Boolean>> filter);

        IQueryable<TEntity> AddOrder(IQueryable<TEntity> query,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);

        IQueryable<TDerivedType> AddOrder<TDerivedType>(IQueryable<TDerivedType> query,
            Func<IQueryable<TDerivedType>, IOrderedQueryable<TDerivedType>> orderBy)
            where TDerivedType : class;

        IQueryable<TEntity> AddFilter(IQueryable<TEntity> query,
            Expression<Func<TEntity, Boolean>> filter);

        IQueryable<TDerivedType> AddFilter<TDerivedType>(IQueryable<TDerivedType> query,
            Expression<Func<TDerivedType, Boolean>> filter)
            where TDerivedType : class;
    }
}
