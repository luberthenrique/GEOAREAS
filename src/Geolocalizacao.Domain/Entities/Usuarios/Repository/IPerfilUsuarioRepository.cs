using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Geolocalizacao.Domain.Entities.Usuarios.Repository
{
    public interface IPerfilUsuarioRepository : IDisposable
    {
        void Update(PerfilUsuario obj);
        Task<PerfilUsuario> GetByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
        IQueryable<PerfilUsuario> GetAll();
        Task<bool> AnyAsync(Expression<Func<PerfilUsuario, bool>> predicate);
        Task<int> SaveChangesAsync();
    }
}
