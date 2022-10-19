using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Geolocalizacao.Application.ViewModels;

namespace Geolocalizacao.Application.Interfaces
{
    public interface IPerfilUsuarioAppService : IDisposable
    {
        Task UpdateAsync(PerfilUsuarioViewModel usuarioViewModel);
        Task RegisterAsync(PerfilUsuarioViewModel usuarioViewModel);
        Task RegisterNewClaims(PerfilUsuarioViewModel perfilUsuarioViewModel);
        Task<PerfilUsuarioViewModel> GetByIdAsync(Guid id);
        Task<PerfilUsuarioViewModel> GetByNameAsync(string name);
        Task<IEnumerable<PerfilUsuarioViewModel>> GetAllAsync();
        Task RemoveAsync(Guid id);
        Task<bool> AnyAsync(Guid id);
        Task<bool> AnyAsync(string name);
    }
}
