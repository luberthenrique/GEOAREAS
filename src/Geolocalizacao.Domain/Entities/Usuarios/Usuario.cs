using Geolocalizacao.Domain.Entities.Clientes;
using Geolocalizacao.Domain.Entities.SetoresCensitarios;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Geolocalizacao.Domain.Entities.Usuarios
{
    public class Usuario : IdentityUser<Guid>
    {
        public Usuario(
            Guid id,
            Guid idPerfil,
            Guid? idUsuario,
            string nome,
            string email,
            bool habilitado)
        {
            Id = id;
            IdPerfil = idPerfil;
            IdUsuario = idUsuario;
            Nome = nome;
            Email = email;
            UserName = email;
            Habilitado = habilitado;
        }

        public new virtual DateTime? LockoutEnd { get; set; }
        public Guid? IdImagem { get; private set; }
        public Guid IdPerfil { get; private set; }
        public Guid? IdUsuario { get; private set; }
        public string Nome { get; set; }
        public bool Habilitado { get; private set; }

        public bool Deleted { get; private set; }

        public virtual ICollection<ApiCliente> ApiCliente_Inclusao { get; private set; }
        public virtual ICollection<ApiCliente> ApiCliente_Alteracao { get; private set; }

        public virtual ICollection<ArquivoSetoresCensitarios> ArquivosSetoresCensitarios { get; private set; }

        public virtual Usuario UsuarioGestor { get; private set; }
        public virtual PerfilUsuario PerfilUsuario { get; private set; }

        public virtual ICollection<Usuario> UsuarioVinculo { get; private set; }

        public void Atualizar(
            Guid idPerfil,
            Guid? idUsuario,
            string nome,
            string email,
            bool habilitado)
        {
            IdPerfil = idPerfil;
            IdUsuario = idUsuario;
            Nome = nome;
            Email = email;
            UserName = email;
            Habilitado = habilitado;
        }

        public void UpdateData(
            string nome)
        {
            Nome = nome;
        }

        public void Deletar()
        {
            Deleted = true;
        }
    }
}
