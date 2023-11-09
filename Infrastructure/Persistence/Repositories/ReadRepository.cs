using Application.Repositories;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System.Linq.Expressions;

namespace Persistence.Repositories
{
    public class ReadRepository<TEntity> : IReadRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly AppDbContext _context;

        public ReadRepository(AppDbContext context)
        {
            _context = context;
        }

        public DbSet<TEntity> Table => _context.Set<TEntity>();

        public IQueryable<TEntity> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }

        public async Task<TEntity> GetByIdAsync(string id, bool tracking = true)
        //=> await Table.FindAsync(Guid.Parse(id));
        {
            var query = Table.AsQueryable();
            if(!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
        }


        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(predicate);
        }


        public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate, bool tracking = true)
        {
            var query = Table.Where(predicate);
            if(!tracking)
                query = query.AsNoTracking();
            
            return query;
        } 

    }
}
