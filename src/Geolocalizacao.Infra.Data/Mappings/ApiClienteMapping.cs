using Geolocalizacao.Domain.Entities.Clientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geolocalizacao.Infra.Data.Mappings
{
    public class ApiClienteMapping : IEntityTypeConfiguration<ApiCliente>
    {
        public void Configure(EntityTypeBuilder<ApiCliente> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(c => c.Nome)                
                .HasColumnType("VARCHAR(70)")
                .IsRequired();

            builder.Property(c => c.ApiKey)
                .HasColumnType("VARCHAR(100)")
                .IsRequired();

            builder.Property(c => c.SecretKey)
                .HasColumnType("VARCHAR(100)")
                .IsRequired();

            builder.HasOne(o => o.Cliente)
             .WithMany(m => m.ApisCliente)
             .HasForeignKey(f => f.IdCliente)
             .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Usuario_Inclusao)
             .WithMany(m => m.ApiCliente_Inclusao)
             .HasForeignKey(f => f.IdUsuarioInclusao)
             .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Usuario_Alteracao)
             .WithMany(m => m.ApiCliente_Alteracao)
             .HasForeignKey(f => f.IdUsuarioAlteracao)
             .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(x => !x.Deleted);
        }
    }
}
