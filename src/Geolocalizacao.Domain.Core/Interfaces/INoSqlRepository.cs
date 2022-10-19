using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Geolocalizacao.Domain.Core.Interfaces
{
    public interface INoSqlRepository<TEntity> : IDisposable where TEntity : class
    {
        Task Add(TEntity obj);
        Task AddRange(IEnumerable<TEntity> obj);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetByIdAsync(Guid id);
        IQueryable<TEntity> GetAll();
        Task UpdateAsync(TEntity obj);
        Task RemoveAsync(Guid id);
        Task<int> SaveChangesAsync();
    }
}
