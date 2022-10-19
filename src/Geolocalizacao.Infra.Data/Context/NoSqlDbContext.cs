using Geolocalizacao.Domain.Core.Models;
using Geolocalizacao.Domain.Entities.SetoresCensitarios;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocalizacao.Infra.Data.Context
{
    public class NoSqlDbContext
    {
        private readonly NoSqlAppConfig _noSqlConfig;
        private readonly MongoClient _mongoClient;
        private IMongoDatabase _database { get; }
        public IClientSessionHandle _session { get; set; }
        private readonly List<Func<Task>> _commands;
        public NoSqlDbContext(IOptions<NoSqlAppConfig> noSqlConfig)
        {
            _noSqlConfig = noSqlConfig.Value;

            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(_noSqlConfig.ConnectionString));
                if (_noSqlConfig.IsSSL)
                {
                    settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
                }
                _mongoClient = new MongoClient(settings);
                _database = _mongoClient.GetDatabase(_noSqlConfig.Database);
                _commands = new List<Func<Task>>();                
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível se conectar com o servidor.", ex);
            }
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }

        public void Dispose()
        {
            while (_session != null && _session.IsInTransaction)
                Thread.Sleep(TimeSpan.FromMilliseconds(100));

            GC.SuppressFinalize(this);
        }

        public async Task<int> SaveChanges()
        {
            using (_session = await _mongoClient.StartSessionAsync())
            {
                _session.StartTransaction();

                var commandTasks = _commands?.Select(c => c());

                if (commandTasks.Any())
                {
                    await Task.WhenAll(commandTasks);
                }

                await _session.CommitTransactionAsync();
            }

            return _commands.Count;
        }

        public void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }

        public void Initialize()
        {           
            if (!BsonClassMap.IsClassMapRegistered(typeof(Geometry)))
            {
                BsonClassMap.RegisterClassMap<Geometry>(p =>
                {
                    p.AutoMap();
                    p.SetIsRootClass(true);
                });

                BsonClassMap.RegisterClassMap<Polygon>();
                BsonClassMap.RegisterClassMap<MultiPolygon>();
            }

            var indexes = _database.GetCollection<Setor>(typeof(Setor).Name).Indexes.List().ToList();
            var indexNames = indexes.SelectMany(index => index.Elements).Where(element => element.Name == "name").Select(name => name.Value.ToString());

            if (!indexNames.Contains("geometry_2dsphere"))
            {
                var index = Builders<Setor>.IndexKeys.Geo2DSphere("geometry");

                _database.GetCollection<Setor>(typeof(Setor).Name).Indexes.CreateOne(index);
            }            
        }
    }
}
