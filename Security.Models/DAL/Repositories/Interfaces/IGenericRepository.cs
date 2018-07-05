using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Security.Models.DAL.GenericRepositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        ModelStateDictionary ModelState { get; set; }

        IQueryable<TEntity> AddFilter(IQueryable<TEntity> query, Expression<Func<TEntity, bool>> filter);
        IQueryable<TDerivedType> AddFilter<TDerivedType>(IQueryable<TDerivedType> query, Expression<Func<TDerivedType, bool>> filter) where TDerivedType : class;
        IQueryable<TEntity> AddOrder(IQueryable<TEntity> query, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        IQueryable<TDerivedType> AddOrder<TDerivedType>(IQueryable<TDerivedType> query, Func<IQueryable<TDerivedType>, IOrderedQueryable<TDerivedType>> orderBy) where TDerivedType : class;
        DbContext BaseContext();
        TType ConcreteContext<TType>() where TType : DbContext;
        bool DeleteBy(Expression<Func<TEntity, bool>> filter);
        TEntity GetBy(Expression<Func<TEntity, bool>> filter, string includeProperties = "");
        TDerivedType GetBy<TDerivedType>(Expression<Func<TDerivedType, bool>> filter, string includeProperties = "") where TDerivedType : class;
        IQueryable<TEntity> List(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Expression<Func<TEntity, bool>> filter = null, string includeProperties = "");
        IEnumerable<TDerivedType> List<TDerivedType>(Func<IQueryable<TDerivedType>, IOrderedQueryable<TDerivedType>> orderBy = null, Expression<Func<TDerivedType, bool>> filter = null) where TDerivedType : class;
        IEnumerable<TEntity> PagedList(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includeProperties = "", Expression<Func<TEntity, bool>> filter = null, int page = 1, int itemsPerPage = 10);
        IEnumerable<TDerivedType> PagedList<TDerivedType>(Func<IQueryable<TDerivedType>, IOrderedQueryable<TDerivedType>> orderBy, string includeProperties = "", Expression<Func<TDerivedType, bool>> filter = null, int page = 1, int itemsPerPage = 10) where TDerivedType : class;
        bool Save(TEntity objToSave);
        bool Update(TEntity objToUpdate);
    }
}