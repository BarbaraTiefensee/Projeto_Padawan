using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Tests.Unit.Domain.Entities;

namespace HBSIS.Padawan.Produtos.Tests.Builders
{
     public class CategoriaBuilderTest
    {
        private int _id;
        private string _nomeCategoria;
        private string _fornecedor;
        private int _fornecedorId;
        private bool _deletado;

        public CategoriaEntity Construir()
        {
            return new CategoriaEntity(_nomeCategoria, _fornecedorId)
            {
                Id = _id
            };
        }

        public CategoriaEntity Export()
        {
            return new CategoriaEntityTest(_nomeCategoria, _fornecedor)
            {
                Id = _id
            };
        }

        public CategoriaBuilderTest ComNomeCategoria(string nomeCategoria)
        {
           _nomeCategoria = nomeCategoria;
            return this;
        }

        public CategoriaBuilderTest ComIdFornecedor(int fornecedorId)
        {
            _fornecedorId = fornecedorId;
            return this;
        }

        public CategoriaBuilderTest Deletar()
        {
            _deletado = true;
            return this;
        }

        public CategoriaBuilderTest ComId(int id)
        {
            _id = id;
            return this;
        }

        public CategoriaBuilderTest ComFornecedor(string fornecedor)
        {
            _fornecedor = fornecedor;
            return this;
        }
    }
}
