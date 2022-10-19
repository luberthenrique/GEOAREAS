using Geolocalizacao.Domain.Core.Commands;
using System;

namespace Geolocalizacao.Domain.Commands.Clientes
{
    public class RegistrarApiClienteCommand : Command
    {
        public Guid IdCliente { get; protected set; }
        public string Nome { get; protected set; }

        public RegistrarApiClienteCommand(
            Guid idCliente,
            string nome)
        {
            IdCliente = idCliente;
            Nome = nome;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
