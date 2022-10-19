using Geolocalizacao.Domain.Commands.Clientes;
using FluentValidation;
using System.Linq;

namespace Geolocalizacao.Domain.Validations.Clientes
{
    public class RegistrarClienteValidation : ClienteValidation<RegistrarClienteCommand>
    {
        public RegistrarClienteValidation()
        {
            Validar();
            ValidarEndereco();
            ValidarContato();
        }
    }
}
