using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SamplesRequest.Models.Models.DAL.DBContext;

namespace SamplesRequest.Models.Models.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal DbSet<TEntity> dbSet;
        protected ModelStateDictionary _ms;
        

        public SamplesRequestDBContext Context { get; set; }

   
        public GenericRepository(SamplesRequestDBContext context)
        {
            Context = context;
            dbSet = Context.Set<TEntity>();
        }

        public ModelStateDictionary ModelState
        {
            get => _ms; 
            set => _ms = value; 
        }

        public TType ConcreteContext<TType>() where TType : DbContext
        {
            return (TType)Convert.ChangeType(Context, typeof(TType));
        }

        public DbContext BaseContext() => Context;
        

        protected void AddErrorMessage(String key, String err)
        {
            _ms?.AddModelError(key, err);
        }

        protected void AddErrorMessage(String key, Exception e)
        {
            _ms?.AddModelError(key, e.Message);
        }

        public virtual IEnumerable<TEntity> PagedList(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, 
                                            string includeProperties = "",
                                            Expression<Func<TEntity, Boolean>> filter = null, int page = 1, int itemsPerPage = 10)
        {

            IQueryable<TEntity> query = dbSet;

            query = AddFilter(query, filter);
            query = AddIncludes(query, includeProperties);
            query = AddOrder(query, orderBy);
            return query.ToList().Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
        }


        public virtual IEnumerable<TDerivedType> PagedList<TDerivedType>(Func<IQueryable<TDerivedType>, 
                                                IOrderedQueryable<TDerivedType>> orderBy, string includeProperties = "",
                                                 Expression<Func<TDerivedType, Boolean>> filter = null, int page = 1, int itemsPerPage = 10) where TDerivedType : class
        {
            IQueryable<TDerivedType> query = dbSet.OfType<TDerivedType>();
            query = AddFilter<TDerivedType>(query, filter);
            query = AddIncludes(query, includeProperties);
            query = AddOrder<TDerivedType>(query, orderBy);
            return query.ToList().Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
        }

        public virtual IQueryable<TEntity> List(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                 Expression<Func<TEntity, Boolean>> filter = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            query = AddFilter(query, filter);
            query = AddIncludes(query, includeProperties);
            query = AddOrder(query, orderBy);

            return query;
        }


        public virtual IEnumerable<TDerivedType> List<TDerivedType>(Func<IQueryable<TDerivedType>, IOrderedQueryable<TDerivedType>> orderBy = null,
                                                 Expression<Func<TDerivedType, Boolean>> filter = null) where TDerivedType : class
        {
            IQueryable<TDerivedType> query = dbSet.OfType<TDerivedType>();
            query = AddFilter<TDerivedType>(query, filter);
            query = AddOrder<TDerivedType>(query, orderBy);
            return query.ToList();
        }

        public virtual TEntity GetBy(Expression<Func<TEntity, Boolean>> filter, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            query = AddIncludes(query, includeProperties);
            return query.Where(filter).FirstOrDefault();
        }

        public virtual TDerivedType GetBy<TDerivedType>(Expression<Func<TDerivedType, Boolean>> filter, string includeProperties = "") where TDerivedType : class
        {
            IQueryable<TDerivedType> query = dbSet.OfType<TDerivedType>();
            query = AddIncludes(query, includeProperties);
            return query.OfType<TDerivedType>().Where(filter).FirstOrDefault();
        }

        public virtual Boolean Save(TEntity objToSave)
        {
            try
            {
                dbSet.Add(objToSave);
                Context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                var message = e.Message + "\n" + e.InnerException != null ? e.InnerException.Message : "";
                AddErrorMessage("error", message);
                return false;
            }
        }

        public virtual Boolean Update(TEntity objToUpdate)
        {
            try
            {
                dbSet.Attach(objToUpdate);

                Context.Entry(objToUpdate).State = EntityState.Modified;
                Context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                var message = e.Message + "\n" + e.InnerException != null ? e.InnerException.Message : "";
                AddErrorMessage("error", message);
                return false;
            }
        }

        public virtual Boolean DeleteBy(Expression<Func<TEntity, Boolean>> filter)
        {
            var obj = GetBy(filter);
            try
            {
                dbSet.Remove(obj);
                Context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                var message = e.Message + "\n" + e.InnerException != null ? e.InnerException.Message : "";
                AddErrorMessage("error", message);
                return false;
            }
        }


        private IQueryable<TEntity> AddIncludes(IQueryable<TEntity> query, string includeProperties)
        {
            foreach (string item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(item);
            }
            return query;
        }

        private IQueryable<TDerivedType> AddIncludes<TDerivedType>(IQueryable<TDerivedType> query, string includeProperties) where TDerivedType : class
        {

            foreach (string item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(item);
            }
            return query;
        }

        #region Helper methods


        public IQueryable<TEntity> AddOrder(IQueryable<TEntity> query, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            if (orderBy == null)
            {
                return query;
            }
            else
            {
                return orderBy(query);
            }
        }

        public IQueryable<TDerivedType> AddOrder<TDerivedType>(IQueryable<TDerivedType> query, Func<IQueryable<TDerivedType>, IOrderedQueryable<TDerivedType>> orderBy) where TDerivedType : class
        {
            if (orderBy == null)
            {
                return query;
            }
            else
            {
                return orderBy(query);
            }
        }

        public IQueryable<TEntity> AddFilter(IQueryable<TEntity> query, Expression<Func<TEntity, Boolean>> filter)
        {
            if (filter == null)
                return query;
            else
                return query.Where(filter);
        }


        public IQueryable<TDerivedType> AddFilter<TDerivedType>(IQueryable<TDerivedType> query, Expression<Func<TDerivedType, Boolean>> filter) where TDerivedType : class
        {
            if (filter == null)
                return query;
            else
                return query.Where(filter);
        }

        #endregion

    }
}
