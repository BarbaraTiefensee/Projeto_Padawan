using HBSIS.Padawan.Produtos.Application.Models;
using HBSIS.Padawan.Produtos.Domain.Entities;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Application.Interfaces
{
    public interface IFornecedorServices
    {
        Task<int> Create(FornecedorRequestModel request);
        Task<FornecedorEntity> Update(int id, FornecedorRequestModel request);
        Task<FornecedorEntity> Delete(int id);
        Task<FornecedorResponseModel> GetById(int id);
    }
}
