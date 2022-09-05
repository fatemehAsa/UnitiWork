using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Models.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {

        private CustomContext _context;
        private GenericRepository<Category> categoryRepository;
        private GenericRepository<Post> postRepository;

        public UnitOfWork(CustomContext context)
        {
            _context = context;
        }


        public GenericRepository<Category> CategoryRepository
        {
            get
            {
                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new GenericRepository<Category>(_context);
                }

                return categoryRepository;
            }
        }


        public GenericRepository<Post> PostRepository
        {
            get
            {
                if (this.postRepository == null)
                {
                    this.postRepository = new GenericRepository<Post>(_context);
                }

                return postRepository;
            }
        }

        public async Task Save(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
