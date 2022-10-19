using Geolocalizacao.Application.ViewModels;
using Geolocalizacao.Service.Configurations;
using Geolocalizacao.Service.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Geolocalizacao.Service.Services
{
    public class BuscaCoordenadasService: BackgroundService
    {
        private readonly ILogger<BuscaCoordenadasService> _logger;
        private readonly WorkerConfig _workerConfig;

        private const string DB_NAME = "Localizacao";
        private const string COLLECTION_NAME = "Bairros";
        private const string COLLECTION_NAME_GEO = "Geolocalizacao";
        private readonly IMongoCollection<Endereco> _dbSet;
        private readonly IMongoCollection<Localizacao> _dbSetGeo;

        public BuscaCoordenadasService(ILogger<BuscaCoordenadasService> logger, WorkerConfig workerConfig, string connectionString)
        {
            _logger = logger;
            _workerConfig = workerConfig;

            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase(DB_NAME);

            _dbSet = database.GetCollection<Endereco>(COLLECTION_NAME);
            _dbSetGeo = database.GetCollection<Localizacao>(COLLECTION_NAME_GEO);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            

            while (!stoppingToken.IsCancellationRequested && _workerConfig.Ativo)
            {
                _logger.LogInformation("Executando serviço de calculo de estatísticas", DateTimeOffset.Now);

                var bairros = await _dbSet.Find(c => c.Cidade == "Juiz de Fora").ToListAsync();

                foreach (var item in bairros)
                {
                    var local = $"{item.Bairro}, {item.Cidade} - {item.UF}";
                    try
                    {
                        if (!_dbSetGeo.Find(c => c.Area == item.Bairro && c.Cidade == item.Cidade && c.UF == item.UF).Any())
                        {
                            var retorno = await BuscarCordenadas(local);

                            foreach (var coordenadas in retorno.Where(c => c.Geojson.Type == "Polygon"))
                            {
                                coordenadas.Id = Guid.NewGuid();
                                coordenadas.Area = item.Bairro;
                                coordenadas.Cidade = item.Cidade;
                                coordenadas.UF = item.UF;

                                coordenadas.Geojson.Coordenadas = JsonSerializer.Deserialize<double[][][]>(coordenadas.Geojson.Coordenadas.ToString());

                                await _dbSetGeo.InsertOneAsync(coordenadas);
                            }
                        }

                        
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }                                    

                }


                _logger.LogInformation("Serviço de calculo de estatísticas finalizado - {time}", DateTimeOffset.Now);

                await Task.Delay(_workerConfig.IntervaloEmHoras * (60 * 60 * 1000), stoppingToken);
            }
        }

        private async Task<List<Localizacao>> BuscarCordenadas(string endereco)
        {
            var host = $"/search.php?q={endereco}&polygon_geojson=1&format=json";

            var client = HttpClientFactory.Create();
            client.BaseAddress = new Uri("https://nominatim.openstreetmap.org");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");

            var response = await client.GetAsync(host);

            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Localizacao>>(json);
        }

    }
}
