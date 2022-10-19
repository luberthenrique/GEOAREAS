using Geolocalizacao.Infra.CrossCutting.GoogleMaps.Models;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocalizacao.Infra.CrossCutting.GoogleMaps.Client
{
    public class GoogleMapsClient : IGoogleMapsClient
    {
        private readonly GoogleSettings _googleSettings;

        /// <summary>
        /// The HTTP client
        /// </summary>
        private readonly HttpClient _httpClient;

        public GoogleMapsClient(
            IOptions<GoogleSettings> googleSettings)
        {
            _googleSettings = googleSettings.Value;

            _httpClient = HttpClientFactory.Create();
            _httpClient.BaseAddress = new Uri(_googleSettings.BaseUri);
        }

        public async Task<Address> BuscarLocalizacao(double latitude, double longitude, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"/maps/api/geocode/json?latlng={latitude.ToString().Replace(",", ".")},{longitude.ToString().Replace(",", ".")}&key={_googleSettings.Key}", cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<GeolocalizacaoResult>(content);

            if (!string.IsNullOrEmpty(result.Error))
            {
                throw new Exception("Occoreu um erro ao buscar localização no google maps: " + result.Error);
            }

            return ConvertreGeolocalizacao_Endereco(result.Results.FirstOrDefault());
        }

        public async Task<Address> BuscarLocalizacao(string endereco, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"/maps/api/geocode/json?address={endereco}&key={_googleSettings.Key}", cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<GeolocalizacaoResult>(content);

            if (!string.IsNullOrEmpty(result.Error))
            {
                throw new Exception("Occoreu um erro ao buscar localização no google maps: " + result.Error);
            }

            return ConvertreGeolocalizacao_Endereco(result.Results.FirstOrDefault());
        }

        private Address ConvertreGeolocalizacao_Endereco(Endereco endereco)
        {
            var address = new Address();

            foreach (var item in endereco.AddressComponents)
            {
                switch (item.Types.FirstOrDefault())
                {
                    case "street_number":
                        address.StreetNumber = item.LongName;
                        break;
                    case "route":
                        address.StreetName = item.LongName;
                        break;
                    case "administrative_area_level_2":
                    case "locality":
                        address.City = item.LongName;
                        break;
                    case "administrative_area_level_1":
                        address.State = item.LongName;
                        break;
                    case "political": case "sublocality_level_1":
                        address.DistrictName = item.LongName;
                        break;
                    case "postal_code":
                        address.Zip = item.LongName;
                        break;
                    case "country":
                        address.Country = item.LongName;
                        break;
                }
            }

            address.Lat = (endereco.Geometry?.Location?.Lat).GetValueOrDefault();
            address.Lng = (endereco.Geometry?.Location?.Lng).GetValueOrDefault();

            return address;
        }
    }
}
