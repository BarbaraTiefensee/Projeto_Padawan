﻿// <auto-generated />
using HBSIS.Padawan.Produtos.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HBSIS.Padawan.Produtos.Infra.Migrations
{
    [DbContext(typeof(MainContext))]
    [Migration("20200728191940_RelacaoNNDasTabelasCategoria")]
    partial class RelacaoNNDasTabelasCategoria
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HBSIS.Padawan.Produtos.Domain.Entities.CategoriaEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deletado")
                        .HasColumnType("bit");

                    b.Property<int>("FornecedorID")
                        .HasColumnName("Fornecedores")
                        .HasColumnType("int");

                    b.Property<string>("NomeCategoria")
                        .IsRequired()
                        .HasColumnName("Nome")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.HasKey("ID");

                    b.HasIndex("FornecedorID");

                    b.ToTable("CATEGORIAS");
                });

            modelBuilder.Entity("HBSIS.Padawan.Produtos.Domain.Entities.FornecedorEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasColumnType("char(18)")
                        .IsFixedLength(true)
                        .HasMaxLength(18)
                        .IsUnicode(false);

                    b.Property<bool>("Deletado")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(70)")
                        .HasMaxLength(70)
                        .IsUnicode(false);

                    b.Property<string>("NomeFantasia")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("RazaoSocial")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("char(11)")
                        .IsFixedLength(true)
                        .HasMaxLength(11)
                        .IsUnicode(false);

                    b.HasKey("ID");

                    b.ToTable("FORNECEDORES");
                });

            modelBuilder.Entity("HBSIS.Padawan.Produtos.Domain.Entities.CategoriaEntity", b =>
                {
                    b.HasOne("HBSIS.Padawan.Produtos.Domain.Entities.FornecedorEntity", "Fornecedor")
                        .WithMany("Categorias")
                        .HasForeignKey("FornecedorID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("HBSIS.Padawan.Produtos.Domain.Entities.FornecedorEntity", b =>
                {
                    b.OwnsOne("HBSIS.Padawan.Produtos.Domain.ComplexType.Endereco", "Endereco", b1 =>
                        {
                            b1.Property<int>("FornecedorEntityID")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Bairro")
                                .IsRequired()
                                .HasColumnName("Fornecedor_Bairro")
                                .HasColumnType("varchar(50)")
                                .IsUnicode(false);

                            b1.Property<string>("CEP")
                                .IsRequired()
                                .HasColumnName("Fornecedor_CEP")
                                .HasColumnType("varchar(8)")
                                .IsUnicode(false);

                            b1.Property<string>("Cidade")
                                .IsRequired()
                                .HasColumnName("Fornecedor_Cidade")
                                .HasColumnType("varchar(40)")
                                .IsUnicode(false);

                            b1.Property<string>("Complemento")
                                .IsRequired()
                                .HasColumnName("Fornecedor_Complemento")
                                .HasColumnType("varchar(70)")
                                .IsUnicode(false);

                            b1.Property<string>("Numero")
                                .IsRequired()
                                .HasColumnName("Fornecedor_Numero")
                                .HasColumnType("varchar(6)")
                                .IsUnicode(false);

                            b1.Property<string>("Rua")
                                .IsRequired()
                                .HasColumnName("Fornecedor_Rua")
                                .HasColumnType("varchar(80)")
                                .IsUnicode(false);

                            b1.Property<string>("UF")
                                .IsRequired()
                                .HasColumnName("Fornecedor_UF")
                                .HasColumnType("varchar(3)")
                                .HasMaxLength(3)
                                .IsUnicode(false);

                            b1.HasKey("FornecedorEntityID");

                            b1.ToTable("FORNECEDORES");

                            b1.WithOwner()
                                .HasForeignKey("FornecedorEntityID");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}