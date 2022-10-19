using Geolocalizacao.Domain.Entities.Clientes;
using Geolocalizacao.Domain.Entities.Usuarios;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace Geolocalizacao.Infra.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<Usuario, PerfilUsuario, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ApiCliente> ApiCliente { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<PerfilUsuario> PerfilUsuario { get; set; }
        //public DbSet<Setor> Setores { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Ignore<ValidationResult>();

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            //builder.ApplyConfiguration(new AreaAbrangenciaMapping());
            //builder.ApplyConfiguration(new PerfilUsuarioMapping());
            ////builder.ApplyConfiguration(new SetorMapping());
            //builder.ApplyConfiguration(new UsuarioMapping());

            base.OnModelCreating(builder);
        }
    }    
}
