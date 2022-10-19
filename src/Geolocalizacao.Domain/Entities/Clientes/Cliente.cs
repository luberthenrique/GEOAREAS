using Geolocalizacao.Domain.Core.Models;
using System;
using System.Collections.Generic;

namespace Geolocalizacao.Domain.Entities.Clientes
{
    public class Cliente : Entity
    {
        public Cliente(
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
            string email,
            Guid idUsuarioInclusao,
            DateTime dataHoraInclusao,
            Guid? idUsuarioAlteracao,
            DateTime? dataHoraAlteracao)
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
            IdUsuarioInclusao = idUsuarioInclusao;
            DataHoraInclusao = dataHoraInclusao;
            IdUsuarioAlteracao = idUsuarioAlteracao;
            DataHoraAlteracao = dataHoraAlteracao;
        }

        protected Cliente()
        {

        }

        public string Cnpj { get; private set; }
        public string InscricaoMunicipal { get; private set; }
        public string RazaoSocial { get; private set; }
        public string Observacao { get; private set; }

        #region Endereco
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Uf { get; private set; }
        public string Cep { get; private set; }
        #endregion

        #region Contato
        public string Telefone1 { get; private set; }
        public string Telefone2 { get; private set; }
        public string Email { get; private set; }
        #endregion

        public Guid IdUsuarioInclusao { get; set; }
        public DateTime DataHoraInclusao { get; set; }
        public Guid? IdUsuarioAlteracao { get; set; }
        public DateTime? DataHoraAlteracao { get; set; }

        public virtual ICollection<ApiCliente> ApisCliente { get; set; }
    }
}
