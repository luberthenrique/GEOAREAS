using Geolocalizacao.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geolocalizacao.Application.Interfaces
{
    public interface IApiClienteAppService : IDisposable
    {
        Task RegisterAsync(ApiClienteViewModel obj);
        Task<IEnumerable<ApiClienteViewModel>> GetByIdClienteAsync(Guid idCliente);
        Task RemoveAsync(Guid id);
        Task<bool> AnyAsync(Guid id);
    }
}
