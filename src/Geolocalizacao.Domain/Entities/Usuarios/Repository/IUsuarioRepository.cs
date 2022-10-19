using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Geolocalizacao.Domain.Entities.Usuarios.Repository
{
    public interface IUsuarioRepository: IDisposable
    {
        IQueryable<Usuario> GetAll();
        void Update(Usuario usuario);
        Task RemoveAsync(Guid id);
        Task<bool> AnyAsync(Expression<Func<Usuario, bool>> predicate);
        Task<bool> AnyAsyncNoQuery(Expression<Func<Usuario, bool>> predicate);
        Task<bool> IsAdmin(Guid id);
        Task<Usuario> GetByEmail(string email);
        Task<IEnumerable<Guid>> ObterSubordinados(Guid id);
        Task<int> SaveChangesAsync();
    }
}
