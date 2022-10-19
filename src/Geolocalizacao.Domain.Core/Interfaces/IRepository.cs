using System;
using System.Linq;
using System.Threading.Tasks;

namespace Geolocalizacao.Domain.Core.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task Add(TEntity obj);
        Task<TEntity> GetByIdAsync(Guid id);
        IQueryable<TEntity> GetAll();
        void Update(TEntity obj);
        Task RemoveAsync(Guid id);
        Task<int> SaveChangesAsync();
    }
}
