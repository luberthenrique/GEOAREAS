using Geolocalizacao.Domain.Entities.SetoresCensitarios;
using Geolocalizacao.Domain.Entities.SetoresCensitarios.Repository;
using Geolocalizacao.Infra.Data.Context;

namespace Geolocalizacao.Infra.Data.Repository
{
    public class ArquivoSetoresCensitariosRepository : Repository<ArquivoSetoresCensitarios>, IArquivoSetoresCensitariosRepository
    {
        public ArquivoSetoresCensitariosRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    }
}
