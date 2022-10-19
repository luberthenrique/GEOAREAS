using Geolocalizacao.Infra.CrossCutting.Geolocalizacao.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;

namespace Geolocalizacao.Infra.CrossCutting.Geolocalizacao.Context
{
    public class GeolocalizacaoDbContext
    {
        private readonly MongoConnection _mongoConnection;
        private IMongoDatabase _database { get; }

        public GeolocalizacaoDbContext(IOptions<MongoConnection> mongoConnection)
        {
            _mongoConnection = mongoConnection.Value;
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(_mongoConnection.ConnectionString));
                if (_mongoConnection.IsSSL)
                {
                    settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
                }
                var mongoClient = new MongoClient(settings);
                _database = mongoClient.GetDatabase(_mongoConnection.Database);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível se conectar com o servidor.", ex);
            }
        }

        public IMongoCollection<Localizacao> Geolocalizacao
        {
            get
            {
                return _database.GetCollection<Localizacao>("Geolocalizacao");
            }
        }
    }
}
