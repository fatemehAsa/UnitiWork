using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Models.UnitOfWork
{
    public class GenericRepository<TEntity> where TEntity:class
    {
        internal CustomContext _context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(CustomContext context)
        {
            _context = context;
            dbSet = context.Set<TEntity>();
        }



       

        public virtual IQueryable<TEntity> GetWithTracking(
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
                return  orderBy(query);
            }
            else
            {
                return  query;
            }
        }

        public virtual IQueryable<TEntity> GetAsNoTracking(
         Expression<Func<TEntity, bool>> filter = null,
         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
         string includeProperties = "")
            => GetWithTracking(filter, orderBy, includeProperties).AsNoTracking();


        public virtual async Task<TEntity> GetByID(object id,CancellationToken cancellationToken)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async void Insert(TEntity entity)
        {
           await dbSet.AddAsync(entity);
        }

        public virtual async void Delete(object id)
        {
            TEntity entityToDelete =await dbSet.FindAsync(id);
            Delete(entityToDelete);
        }

        public virtual  void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual async Task<bool> IsExist(object key,CancellationToken cancellationToken)
        {
            return await dbSet.FindAsync(key,cancellationToken) != null;
        }

    }
}
