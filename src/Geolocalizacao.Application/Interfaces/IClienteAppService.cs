using Geolocalizacao.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geolocalizacao.Application.Interfaces
{
    public interface IClienteAppService : IDisposable
    {
        Task<ClienteViewModel> UpdateAsync(ClienteViewModel obj);
        Task<ClienteViewModel> RegisterAsync(ClienteViewModel obj);
        Task<ClienteViewModel> GetByIdAsync(Guid id);
        Task<ClienteViewModel> GetByCnpjAsync(string cnpj);
        Task<bool> AnyAsync(string cnpj);
        Task<IEnumerable<ClienteViewModel>> GetAllAsync();
        Task RemoveAsync(Guid id);
        Task<bool> AnyAsync(Guid id);
    }
}
