using Geolocalizacao.Domain.Entities.Clientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Geolocalizacao.Infra.Data.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(c => c.Cnpj)                
                .HasColumnType("VARCHAR(14)")
                .IsRequired();

            builder.Property(c => c.InscricaoMunicipal)
                .HasColumnType("VARCHAR(18)")
                .IsRequired();

            builder.Property(c => c.RazaoSocial)
                .HasColumnType("VARCHAR(300)")
                .IsRequired();

            builder.Property(c => c.Observacao)
            .HasColumnType("VARCHAR(300)");

            builder.Property(c => c.Logradouro)
               .HasColumnType("VARCHAR(120)")
               .IsRequired();
            builder.Property(c => c.Numero)
                .HasColumnType("VARCHAR(20)")
                .IsRequired();
            builder.Property(c => c.Complemento)
                .HasColumnType("VARCHAR(60)")
                .IsRequired();
            builder.Property(c => c.Bairro)
                .HasColumnType("VARCHAR(200)")
                .IsRequired();
            builder.Property(c => c.Cidade)
                .HasColumnType("VARCHAR(200)")
                .IsRequired();
            builder.Property(c => c.Uf)
                .HasColumnType("CHAR(2)")
                .IsRequired();
            builder.Property(c => c.Cep)
                .HasColumnType("VARCHAR(8)")
                .IsRequired();

            builder.Property(c => c.Telefone1)
                .HasColumnType("VARCHAR(11)")
                .IsRequired();

            builder.Property(c => c.Telefone2)
                .HasColumnType("VARCHAR(11)");

            builder.Property(c => c.Email)
                .HasColumnType("VARCHAR(254)")
                .IsRequired();

            builder.HasQueryFilter(x => !x.Deleted);
        }
    }
}
