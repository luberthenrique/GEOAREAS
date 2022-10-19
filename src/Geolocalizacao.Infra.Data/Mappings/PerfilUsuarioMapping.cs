using Geolocalizacao.Domain.Entities.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geolocalizacao.Infra.Data.Mappings
{
    public class PerfilUsuarioMapping : IEntityTypeConfiguration<PerfilUsuario>
    {
        public void Configure(EntityTypeBuilder<PerfilUsuario> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.HasIndex(e => e.NormalizedName)
                .IsUnique(false);

            builder.HasQueryFilter(x => !x.Deleted);
        }
    }
}
