using Geolocalizacao.Domain.Validations.Clientes;
using System;

namespace Geolocalizacao.Domain.Commands.Clientes
{
    public class AlterarClienteCommand : ClienteCommand
    {
        public AlterarClienteCommand(
            Guid id,
            string cnpj,
            string inscricaoMunicipal,
            string razaoSocial,
            string observacao,
            string logradouro,
            string numero,
            string complemento,
            string bairro,
            string cidade,
            string uf,
            string cep,
            string telefone1,
            string telefone2,
            string email)
        {
            Id = id;
            Cnpj = cnpj;
            InscricaoMunicipal = inscricaoMunicipal;
            RazaoSocial = razaoSocial;
            Observacao = observacao;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Uf = uf;
            Cep = cep;
            Telefone1 = telefone1;
            Telefone2 = telefone2;
            Email = email;
        }

        public override bool IsValid()
        {
            ValidationResult = new AlterarClienteValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
