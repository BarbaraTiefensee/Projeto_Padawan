using HBSIS.Padawan.Produtos.Domain.Entities;

namespace HBSIS.Padawan.Produtos.Application.Models.CategoriaModel
{
    public class CategoriaResponseModel : CategoriaModelBase
    {
        public CategoriaResponseModel(CategoriaEntity categoria)
        {
            NomeCategoria = categoria.NomeCategoria;
            FornecedorId = categoria.FornecedorId;
            Id = categoria.Id;
        }

        public static CategoriaResponseModel Converter(CategoriaEntity categoria)
        {
            return new CategoriaResponseModel(categoria);
        }
    }
}
