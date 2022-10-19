using Geolocalizacao.Domain.Core.Models;
using Geolocalizacao.Domain.Entities.Usuarios;
using System;

namespace Geolocalizacao.Domain.Entities.Clientes
{
    public class ApiCliente : Entity
    {
        public ApiCliente(
            Guid id,
            Guid idCliente,
            string nome, 
            string apiKey, 
            string secretKey, 
            Guid idUsuarioInclusao, 
            DateTime dataHoraInclusao, 
            Guid? idUsuarioAlteracao, 
            DateTime? dataHoraAlteracao)
        {
            Id = id;
            IdCliente = idCliente;
            Nome = nome;
            ApiKey = apiKey;
            SecretKey = secretKey;
            IdUsuarioInclusao = idUsuarioInclusao;
            DataHoraInclusao = dataHoraInclusao;
            IdUsuarioAlteracao = idUsuarioAlteracao;
            DataHoraAlteracao = dataHoraAlteracao;
        }

        public Guid IdCliente { get; private set; }
        public string Nome { get; private set; }
        public string ApiKey { get; private set; }
        public string SecretKey { get; private set; }
        public Guid IdUsuarioInclusao { get; private set; }
        public DateTime DataHoraInclusao { get; private set; }
        public Guid? IdUsuarioAlteracao { get; private set; }
        public DateTime? DataHoraAlteracao { get; private set; }

        public virtual Cliente Cliente { get; private set; }

        public virtual Usuario Usuario_Inclusao { get; private set; }
        public virtual Usuario Usuario_Alteracao { get; private set; }
    }
}
