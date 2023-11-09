using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IWriteRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<bool> AddAsync(TEntity entity);
        Task<bool> AddRangeAsync(List<TEntity> entity);
        bool Update(TEntity entity);
        bool Remove(TEntity entity);
        Task<bool> RemoveAsync(string id);
        bool RemoveRange(List<TEntity> entities);
        Task<int> SaveAsync();
    }
}
