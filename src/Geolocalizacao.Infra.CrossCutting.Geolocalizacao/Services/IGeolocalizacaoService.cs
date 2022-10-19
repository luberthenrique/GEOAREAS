using Geolocalizacao.Infra.CrossCutting.Geolocalizacao.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geolocalizacao.Infra.CrossCutting.Geolocalizacao.Services
{
    public interface IGeolocalizacaoService
    {
        Task<List<Localizacao>> GetByLocal(string cidade, string estado);
    }
}
