using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Geolocalizacao.Domain.Entities.Usuarios
{
    public class PerfilUsuario : IdentityRole<Guid>
    {
        public PerfilUsuario()
        {
        }

        public PerfilUsuario(
            Guid id,
            bool isAdmin,
            string nome,
            string concurrencyStamp
            )
        {
            Id = id;
            IsAdmin = isAdmin;
            Name = nome;
            NormalizedName = Name.ToUpper();
            ConcurrencyStamp = concurrencyStamp;
        }

        public bool IsAdmin { get; set; }
        public bool Deleted { get; private set; }

        public virtual ICollection<Usuario> Usuario { get; private set; }
        public void Atualizar(
            bool isAdmin,
            string nome
            )
        {
            IsAdmin = isAdmin;
            Name = nome;
            NormalizedName = nome.ToUpper();
        }

        public void Deletar()
        {
            this.Deleted = true;
        }
    }
}
