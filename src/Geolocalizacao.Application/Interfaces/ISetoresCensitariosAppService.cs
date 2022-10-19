using Geolocalizacao.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geolocalizacao.Application.Interfaces
{
    public interface ISetoresCensitariosAppService : IDisposable
    {
        Task RegisterAsync(ArquivoSetoresCensitariosUploadViewModel obj);
        Task<IEnumerable<ArquivoSetoresCensitariosViewModel>> GetAllAsync();
        Task CerragarArquivo(ArquivoSetoresCensitariosUploadViewModel obj);
    }
}
