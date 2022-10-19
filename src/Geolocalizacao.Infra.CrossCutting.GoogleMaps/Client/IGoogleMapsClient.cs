using Geolocalizacao.Infra.CrossCutting.GoogleMaps.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocalizacao.Infra.CrossCutting.GoogleMaps.Client
{
    public interface IGoogleMapsClient
    {
        Task<Address> BuscarLocalizacao(double latitude, double longitude, CancellationToken cancellationToken);
        Task<Address> BuscarLocalizacao(string endereco, CancellationToken cancellationToken);
    }
}
