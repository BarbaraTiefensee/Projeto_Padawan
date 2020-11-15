using FluentAssertions;
using HBSIS.Padawan.Produtos.Tests.Builders;
using Xunit;

namespace HBSIS.Padawan.Produtos.Tests.Unit.Domain.Entities
{
    public class CategoriaTests
    {
        [Fact]
        public void Pegar_dados_Categoria()
        {
            var categoriaID = 1;
            var idDoFornecedor = 4;
            var categoria = new CategoriaBuilderTest()
                                  .ComNomeCategoria("Bebida")
                                  .ComIdFornecedor(idDoFornecedor)
                                  .ComId(categoriaID)
                                  .Construir();

            categoria.NomeCategoria.Should().Be("Bebida");
            categoria.FornecedorId.Should().Be(idDoFornecedor);
            categoria.Id.Should().Be(categoriaID);
        }
        [Fact]
        public void Deletar_Categoria()
        {
            var categoriaID = 1;
            var idDoFornecedor = 4;
            var categoria = new CategoriaBuilderTest()
                .ComNomeCategoria("Bebidas")
                .ComIdFornecedor(idDoFornecedor)
                .ComId(categoriaID)
                .Construir();

            categoria.Delete();
            categoria.Deletado.Should().BeTrue();
        }
        [Fact]
        public void Atualizar_Categoria()
        {
            var categoriaID = 1;
            var idDoFornecedor = 4;
            var categoria = new CategoriaBuilderTest()
                .ComNomeCategoria("Bebidas")
                .ComIdFornecedor(idDoFornecedor)
                .ComId(categoriaID)
                .Construir();

            categoria.Update("agua", idDoFornecedor);
        }
    }
}