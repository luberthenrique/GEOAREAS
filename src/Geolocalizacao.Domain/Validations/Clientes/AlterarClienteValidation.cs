using Geolocalizacao.Domain.Commands.Clientes;

namespace Geolocalizacao.Domain.Validations.Clientes
{
    public class AlterarClienteValidation : ClienteValidation<AlterarClienteCommand>
    {
        public AlterarClienteValidation()
        {
            ValidarId();
            Validar();
            ValidarEndereco();
            ValidarContato();
        }
    }
}