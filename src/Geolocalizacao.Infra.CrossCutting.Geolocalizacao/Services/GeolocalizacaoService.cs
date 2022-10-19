using Geolocalizacao.Infra.CrossCutting.Geolocalizacao.Context;
using Geolocalizacao.Infra.CrossCutting.Geolocalizacao.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geolocalizacao.Infra.CrossCutting.Geolocalizacao.Services
{
    public class GeolocalizacaoService : IGeolocalizacaoService
    {
        private readonly IMongoCollection<Localizacao> DbSet;

        public GeolocalizacaoService(GeolocalizacaoDbContext dbContext)
        {
            DbSet = dbContext.Geolocalizacao;
        }

        public async Task<List<Localizacao>> GetByLocal(string cidade, string estado)
        {
            return await DbSet.Find(c => c.Cidade == cidade && c.UF == estado).ToListAsync();
        }
    }
}
