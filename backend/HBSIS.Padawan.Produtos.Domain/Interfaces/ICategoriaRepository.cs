using HBSIS.Padawan.Produtos.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Domain.Interfaces
{
    public interface ICategoriaRepository : IGenericRepository<CategoriaEntity>
    {
        Task<bool> VerificarSeJaExisteCategoria(string nomeCategoria);
        Task<bool> VerificarSeExisteCategoriaComEsseNome(string nomeCategoria, int fornecedorId);
        Task<List<CategoriaEntity>> GetAllExportCsv();
    }
}
