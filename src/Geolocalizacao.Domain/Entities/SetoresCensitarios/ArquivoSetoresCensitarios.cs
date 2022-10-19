using Geolocalizacao.Domain.Core.Models;
using Geolocalizacao.Domain.Entities.Usuarios;
using Geolocalizacao.Domain.Enumerables;
using System;

namespace Geolocalizacao.Domain.Entities.SetoresCensitarios
{
    public class ArquivoSetoresCensitarios : Entity
    {
        protected ArquivoSetoresCensitarios()
        {

        }

        public ArquivoSetoresCensitarios(
            Guid id,
            StatusArquivoSetorCensitario status, 
            string nome,
            string nomeArquivo, 
            DateTime dataInclusao, 
            Guid idUsuarioInclusao)
        {
            Id = id;
            Status = (int)status;
            Nome = nome;
            NomeArquivo = nomeArquivo;
            DataInclusao = dataInclusao;
            IdUsuarioInclusao = idUsuarioInclusao;
        }

        public void UpdateStatus(int status)
        {
            Status = status;
        }

        public int Status { get; private set; }
        public string Nome { get; private set; }
        public string NomeArquivo { get; private set; }
        public string Erro { get; private set; }
        public DateTime DataInclusao { get; private set; }
        public Guid IdUsuarioInclusao { get; private set; }

        public virtual Usuario UsuarioInclusao { get; private set; }

        public void AtualizarStatus(StatusArquivoSetorCensitario status, string erro = null)
        {
            Status = (int)status;
        }
    }
}
