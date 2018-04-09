using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DAL
{
    interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll(string[] includes = null);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includeProperties);
        TEntity GetByID(object id);        
        void Delete(string id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);     
        void Add(TEntity entity);
    }
}
