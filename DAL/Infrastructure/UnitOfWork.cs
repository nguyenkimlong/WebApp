using System;
using System.IO;
using System.Linq;
using DataAccess;
using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DAL.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private ShopDbContext context = new ShopDbContext();
        private Dictionary<string, dynamic> _repositories;

        public UnitOfWork(ShopDbContext context)
        {
            if (context == null || this.disposed)
            {
                context = new ShopDbContext();
                this._repositories = null;
            }
            this.context = context;
        }


        public GenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {            
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, dynamic>();
            }

            var type = typeof(TEntity).Name;
            if (_repositories.ContainsKey(type))
            {
                return (GenericRepository<TEntity>)_repositories[type];
            }

            _repositories.Add(type, new GenericRepository<TEntity>(context));


            return _repositories[type];
        }

        public void SaveChanges()
        {
            context.SaveChanges();
            foreach (var dbEntityEntry in context.ChangeTracker.Entries())
            {
                if (dbEntityEntry.Entity != null)
                {
                    dbEntityEntry.State = EntityState.Detached;
                }
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void RejectChanges()
        {
            foreach (var entry in context.ChangeTracker.Entries()
                  .Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }
    }
}
