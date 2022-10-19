using Geolocalizacao.Domain.Entities.SetoresCensitarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geolocalizacao.Infra.Data.Mappings
{
    public class ArquivoSetoresCensitariosMapping : IEntityTypeConfiguration<ArquivoSetoresCensitarios>
    {
        public void Configure(EntityTypeBuilder<ArquivoSetoresCensitarios> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Nome)
                .HasColumnType("varchar(100)");

            builder.Property(p => p.NomeArquivo)
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Erro)
                .HasColumnType("varchar(200)");

            builder.HasOne(o => o.UsuarioInclusao)
                         .WithMany(m => m.ArquivosSetoresCensitarios)
                         .HasForeignKey(f => f.IdUsuarioInclusao)
                         .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(x => !x.Deleted);
        }
    }
}