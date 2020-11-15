using HBSIS.Padawan.Produtos.Application.Models.CategoriaModel;
using HBSIS.Padawan.Produtos.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Services.CategoriaServices
{
    public interface ICategoriaServices
    {
        Task<int> Create(CategoriaRequestModel request);
        Task<CategoriaEntity> Update(int id, CategoriaRequestModel request);
        Task<CategoriaEntity> Delete(int id);
        Task<IEnumerable<CategoriaResponseModel>> GetAll();
        Task<CategoriaResponseModel> GetById(int id);
        Task<string> ExportCategoria();
        Task<IEnumerable<CategoriaEntity>> ImportCategoria(IFormFile caminhoArquivo);
    }
}
