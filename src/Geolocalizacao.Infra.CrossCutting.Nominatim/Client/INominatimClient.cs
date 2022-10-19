using Geolocalizacao.Infra.CrossCutting.Nominatim.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geolocalizacao.Infra.CrossCutting.Nominatim.Client
{
    public interface INominatimClient
    {
        Task<Polygon> ObterPoligono(string query);
        Task<Polygon> ObterPoligono(string uf, string localidade);
        Task<Polygon> ObterPoligono(string uf, string localidade, string bairro);
    }
}
