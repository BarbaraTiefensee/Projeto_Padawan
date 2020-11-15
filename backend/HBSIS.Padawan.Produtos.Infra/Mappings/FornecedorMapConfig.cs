using HBSIS.Padawan.Produtos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace HBSIS.Padawan.Produtos.Infra.Mappings
{
    public class FornecedorMapConfig : IEntityTypeConfiguration<FornecedorEntity>
    {
        public void Configure(EntityTypeBuilder<FornecedorEntity> builder)
        {
            builder.ToTable("FORNECEDORES");
            builder.Property(f => f.CNPJ).IsFixedLength().HasMaxLength(18).IsRequired();
         
            builder.Property(f => f.Email).IsRequired().HasMaxLength(70);

            builder.Property(f => f.NomeFantasia).HasMaxLength(100).IsRequired();

            builder.Property(f => f.RazaoSocial).IsRequired().HasMaxLength(100);

            builder.Property(f => f.Telefone).IsFixedLength().HasMaxLength(11).IsRequired();
            

            builder.OwnsOne(f => f.Endereco, endereco =>
            {
                endereco.Property(f => f.Rua)
                    .IsRequired()
                    .HasColumnName("Fornecedor_Rua")
                    .HasColumnType("varchar(80)");

                endereco.Property(f => f.Bairro)
                    .IsRequired()
                    .HasColumnName("Fornecedor_Bairro")
                    .HasColumnType("varchar(50)");

                endereco.Property(f => f.CEP)
                    .IsRequired()
                    .HasColumnName("Fornecedor_CEP")
                    .HasColumnType("varchar(8)");
             
                endereco.Property(f => f.Cidade)
                    .IsRequired()
                    .HasColumnName("Fornecedor_Cidade")
                    .HasColumnType("varchar(40)");

                endereco.Property(f => f.Complemento)
                    .IsRequired()
                    .HasColumnName("Fornecedor_Complemento")
                    .HasColumnType("varchar(70)");

                endereco.Property(f => f.Numero)
                    .IsRequired()
                    .HasColumnName("Fornecedor_Numero")
                    .HasColumnType("varchar(6)");

                endereco.Property(f => f.UF)
                    .IsRequired()
                    .HasColumnName("Fornecedor_UF")
                    .HasMaxLength(3);
            });
        }
    }
}
