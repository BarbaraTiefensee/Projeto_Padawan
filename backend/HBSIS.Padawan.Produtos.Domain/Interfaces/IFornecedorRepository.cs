using HBSIS.Padawan.Produtos.Domain.Entities;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Domain.Interfaces
{
    public interface IFornecedorRepository : IGenericRepository<FornecedorEntity>
    {
        Task<bool> VerificarSeFornecedorJaExiste(string cnpj);
        Task<bool> ExisteFornecedorComEsseCnpj(string cnpj, int id);
    }
}
