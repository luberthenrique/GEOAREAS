using Geolocalizacao.Domain.Commands.Clientes;

namespace Geolocalizacao.Domain.Validations.Clientes
{
    public class RemoverClienteValidation : ClienteValidation<RemoverClienteCommand>
    {
        public RemoverClienteValidation()
        {
            ValidarId();
        }
    }
}
