using System;

namespace Geolocalizacao.Domain.Commands.Clientes
{
    public class RemoverClienteCommand : ClienteCommand
    {
        public RemoverClienteCommand(
            Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
