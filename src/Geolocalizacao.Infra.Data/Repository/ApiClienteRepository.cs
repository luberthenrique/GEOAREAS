using Geolocalizacao.Domain.Entities.Clientes;
using Geolocalizacao.Domain.Entities.Clientes.Repository;
using Geolocalizacao.Infra.Data.Context;

namespace Geolocalizacao.Infra.Data.Repository
{
    public class ApiClienteRepository : Repository<ApiCliente>, IApiClienteRepository
    {
        public ApiClienteRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
