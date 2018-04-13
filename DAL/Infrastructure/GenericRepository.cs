using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using DataAccess;
using DAL;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DAL.Infrastructure
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal ShopDbContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(ShopDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public IEnumerable<TEntity> GetAll(string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = context.Set<TEntity>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.AsQueryable();
            }

            return context.Set<TEntity>().AsQueryable();
        }
        public IQueryable<TEntity> GetQuery<TEntity>() where TEntity : class
        {
            return context.Set<TEntity>().AsNoTracking();
        }


        /// <summary>
        /// Get Entity queryable & Include all input tables
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="tables"></param>
        /// <returns></returns>
        public IQueryable<TEntity> GetQuery<TEntity>(params string[] tables) where TEntity : class
        {
            var context = GetQuery<TEntity>();
            if (tables != null)
            {
                foreach (string table in tables)
                {
                    context = (context).Include(table);
                }
            }

            return context;
        }


        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);

        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Update(entityToUpdate);
        }
    }
}
