using HBSIS.Padawan.Produtos.Domain.Entities;

namespace HBSIS.Padawan.Produtos.Application.Models.CategoriaModel
{
    public abstract class CategoriaModelBase : BaseEntity
    {
        public string NomeCategoria { get; set; }
        public int FornecedorId { get; set; }
    }
}
