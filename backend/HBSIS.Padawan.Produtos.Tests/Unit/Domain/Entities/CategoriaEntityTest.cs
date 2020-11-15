using HBSIS.Padawan.Produtos.Domain.Entities;

namespace HBSIS.Padawan.Produtos.Tests.Unit.Domain.Entities
{
    public class CategoriaEntityTest : CategoriaEntity
    {
        public CategoriaEntityTest(string nomeCategoria, string fornecedor)
        {
            this.NomeCategoria = nomeCategoria;
            this.Fornecedor = new FornecedorEntity();
            this.Fornecedor.NomeFantasia = fornecedor;
        }
    }
}
