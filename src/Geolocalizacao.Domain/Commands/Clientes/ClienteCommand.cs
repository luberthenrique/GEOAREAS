using Geolocalizacao.Domain.Core.Commands;
using System;

namespace Geolocalizacao.Domain.Commands.Clientes
{
    public abstract class ClienteCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Cnpj { get; protected set; }
        public string InscricaoMunicipal { get; protected set; }
        public string RazaoSocial { get; protected set; }
        public string Observacao { get; protected set; }

        #region Endereco
        public string Logradouro { get; protected set; }
        public string Numero { get; protected set; }
        public string Complemento { get; protected set; }
        public string Bairro { get; protected set; }
        public string Cidade { get; protected set; }
        public string Uf { get; protected set; }
        public string Cep { get; protected set; }
        #endregion

        #region Contato
        public string Telefone1 { get; protected set; }
        public string Telefone2 { get; protected set; }
        public string Email { get; protected set; }
        #endregion
    }
}
