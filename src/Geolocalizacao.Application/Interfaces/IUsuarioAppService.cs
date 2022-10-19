using Geolocalizacao.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geolocalizacao.Application.Interfaces
{
    public interface IUsuarioAppService : IDisposable
    {
        Task UpdateAsync(UsuarioViewModel usuarioViewModel);
        Task UpdateDataAsync(UsuarioDataViewModel usuario);
        Task RegisterAsync(UsuarioViewModel usuarioViewModel);
        Task RegisterConfirmedUserAsync(UsuarioViewModel usuarioViewModel, string senha, string role);
        Task<UsuarioViewModel> GetByIdAsync(Guid id);
        Task<UsuarioViewModel> GetByEmailAsync(string email);
        Task<IEnumerable<UsuarioViewModel>> GetAllAsync();
        Task<Guid?> GetIdProfileAsync(Guid idUser);
        Task RemoveAsync(Guid id);
        Task<bool> AnyAsync(Guid id);
        Task<bool> AnyAsync(string email);
    }
}
