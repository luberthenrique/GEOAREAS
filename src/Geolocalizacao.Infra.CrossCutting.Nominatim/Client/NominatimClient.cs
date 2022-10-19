using Geolocalizacao.Infra.CrossCutting.Nominatim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Geolocalizacao.Infra.CrossCutting.Nominatim.Client
{
    public class NominatimClient : INominatimClient
    {

        public NominatimClient()
        {            
        }
        public async Task<Polygon> ObterPoligono(string query)
        {
            return await SendAsync($"q={query}");
        }

        public async Task<Polygon> ObterPoligono(string uf, string localidade)
        {
            return await SendAsync($"city={localidade}&state={uf}");
        }

        public async Task<Polygon> ObterPoligono(string uf, string localidade, string bairro)
        {
            return await SendAsync($"q={bairro}, {localidade} - {uf}");
        }

        private async Task<Polygon> SendAsync(string paramsString)
        {
            var httpClient = HttpClientFactory.Create();
            httpClient.BaseAddress = new Uri("https://nominatim.openstreetmap.org");

            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");

            var response = await httpClient.GetAsync($"/search?{paramsString}&polygon_geojson=1&format=json");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<Polygon>>(json);
            var poligono = result.FirstOrDefault(c=> c.Type == "administrative");

            if (poligono == null)
                return null;

            if (poligono.Geojson.Type == "Polygon")
            {
                try
                {
                    var coordenadas = JsonSerializer.Deserialize<double[][][]>(poligono.Geojson.Coordinates.ToString());
                    poligono.Geojson.Coordinates = coordenadas.First();
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
            else if (poligono.Geojson.Type == "MultiPolygon")
            {
                var coordenadas = JsonSerializer.Deserialize<double[][][][]>(poligono.Geojson.Coordinates.ToString());
                var x = coordenadas.First();
                var y = x.First();
                poligono.Geojson.Coordinates = coordenadas.First().First();
            }

            return poligono;
        }
    }
}
