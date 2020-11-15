using HBSIS.Padawan.Produtos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HBSIS.Padawan.Produtos.Infra.Mappings
{
    public class CategoriaMapConfig : IEntityTypeConfiguration<CategoriaEntity>
    {
        public void Configure(EntityTypeBuilder<CategoriaEntity> builder)
        {
            builder.ToTable("CATEGORIAS");

            builder.Property(c => c.NomeCategoria)
                .HasColumnName("Nome")
                .HasMaxLength(50)
                .IsRequired();

            builder.HasOne<FornecedorEntity>(c => c.Fornecedor)
                .WithMany(c => c.Categorias)
                .HasForeignKey(c => c.FornecedorId);
        }
    }
}
