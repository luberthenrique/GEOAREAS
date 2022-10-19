using Geolocalizacao.Domain.Core.Models;
using Geolocalizacao.Domain.Entities.SetoresCensitarios;
using Geolocalizacao.Domain.Entities.SetoresCensitarios.Repository;
using Geolocalizacao.Infra.Data.Context;
using Geolocalizacao.Infra.Data.Repository.Base;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Geolocalizacao.Infra.Data.Repository
{
    public class SetorRepository : NoSqlRepository<Setor>, ISetorRepository
    {
        public SetorRepository(NoSqlDbContext dbContext) : base(dbContext)
        {

        }

        public List<Setor> Find(Expression<Func<Setor, bool>> expression)
        {
            return _context.GetCollection<Setor>(typeof(Setor).Name).Find(Builders<Setor>.Filter.Where(expression)).ToList();
        }

        public List<Setor> GetByDistance(double latitude, double longitude, double? maxDistance = null)
        {
            string query = "{geometry:{ " +
                "$near:{" +
                "$geometry: { " +
                "type: \"Point\",  coordinates:[ " + $"{longitude.ToString().Replace(",", ".")}, {latitude.ToString().Replace(",", ".")}" + " ] " +
                "}," +
                $"$maxDistance: {maxDistance}" +
                "}}}";

            var filter = BsonDocument.Parse(query);
            return _context.GetCollection<Setor>(typeof(Setor).Name).Find(filter).ToList();
        }

        public DeleteResult DeletarSetores(string uf)
        {
            return _context.GetCollection<Setor>(typeof(Setor).Name).DeleteMany(c => c.Uf == uf);
        }
    }
}
