using FluentValidation;
using HBSIS.Padawan.Produtos.Domain.Validation;

namespace HBSIS.Padawan.Produtos.Domain.Entities
{
    public class CategoriaEntity : BaseEntity
    {
        public string NomeCategoria { get; protected set; }
        public int FornecedorId { get; protected set; }
        public virtual FornecedorEntity Fornecedor { get; protected set; }

        protected CategoriaEntity()
        {
        }

        public CategoriaEntity(string nomeCategoria, int fornecedorId)
        {
            NomeCategoria = nomeCategoria;
            FornecedorId = fornecedorId;
            Validate();
        }

        public void Update(string nomeCategoria, int fornecedorId)
        {
            NomeCategoria = nomeCategoria;
            FornecedorId = fornecedorId;
            Validate();
        }

        public void Delete()
        {
            Deletado = true;
        }

        public void Validate()
        {
            var categoriaValidator = new CategoriaValidator();
            categoriaValidator.ValidateAndThrow(this);
        }
    }
}
