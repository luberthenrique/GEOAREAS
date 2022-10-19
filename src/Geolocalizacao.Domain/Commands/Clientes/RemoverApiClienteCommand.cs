using Geolocalizacao.Domain.Core.Commands;
using System;

namespace Geolocalizacao.Domain.Commands.Clientes
{
    public class RemoverApiClienteCommand : Command
    {
        public Guid Id { get; private set; }
        public RemoverApiClienteCommand(
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
