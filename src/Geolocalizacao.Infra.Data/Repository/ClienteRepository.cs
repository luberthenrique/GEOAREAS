using Geolocalizacao.Domain.Entities.Clientes;
using Geolocalizacao.Domain.Entities.Clientes.Repository;
using Geolocalizacao.Infra.Data.Context;

namespace Geolocalizacao.Infra.Data.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
