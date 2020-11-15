using HBSIS.Padawan.Produtos.Domain.Entities;

namespace HBSIS.Padawan.Produtos.Application.Models.CategoriaModel
{
    public class CategoriaRequestModel : CategoriaModelBase
    {
        public CategoriaEntity ConverterParaCategoriaEntity()
        {
            return new CategoriaEntity(NomeCategoria, FornecedorId);
        }
    }
}
