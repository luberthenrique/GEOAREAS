using Geolocalizacao.Domain.Entities.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geolocalizacao.Infra.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(e => e.Email)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.LockoutEnd)
                .HasColumnType("DateTime");

            builder.HasOne(o => o.PerfilUsuario)
             .WithMany(m => m.Usuario)
             .HasForeignKey(f => f.IdPerfil)
             .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.UsuarioGestor)
             .WithMany(m => m.UsuarioVinculo)
             .HasForeignKey(f => f.IdUsuario)
             .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(e => e.NormalizedEmail)
                .IsUnique();

            builder.HasQueryFilter(x => !x.Deleted);
        }
    }
}
