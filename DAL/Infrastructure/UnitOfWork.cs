using System;
using System.Linq;
using DataAccess;
using DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ShopDbContext context = new ShopDbContext();
        private GenericRepository<Post> postRepository;     
        private GenericRepository<PostCategory> postCategory;

        public GenericRepository<Post> PostRepository
        {
            get
            {

                if (this.postRepository == null)
                {
                    this.postRepository = new GenericRepository<Post>(context);
                }
                return postRepository;
            }
        }

        public GenericRepository<PostCategory> PostCategoryRepository
        {
            get
            {

                if (this.postCategory == null)
                {
                    this.postCategory = new GenericRepository<PostCategory>(context);
                }
                return postCategory;
            }
        }

        public void SaveChanges()
        {
            context.SaveChanges();
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
