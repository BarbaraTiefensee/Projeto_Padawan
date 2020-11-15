using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Infra.Context;
using HBSIS.Padawan.Produtos.Infra.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Infra.Repository.CategoriaRepository
{
    public class CategoriaRepository : GenericRepository<CategoriaEntity>, ICategoriaRepository
    {
        private readonly MainContext _context;

        public CategoriaRepository(MainContext context) : base(context)
        {
        }

        public async Task<bool> VerificarSeExisteCategoriaComEsseNome(string nomeCategoria, int fornecedorId)
        {
            return await _dbSet.AnyAsync(x => x.NomeCategoria == nomeCategoria && x.Id != fornecedorId && x.Deletado != true);
        }

        public async Task<bool> VerificarSeJaExisteCategoria(string nomeCategoria)
        {
            return await _dbSet.AnyAsync(x => x.NomeCategoria == nomeCategoria && x.Deletado == false);
        }

        public async Task<List<CategoriaEntity>> GetAllExportCsv()
        {
            return await Query().Include(c => c.Fornecedor).ToListAsync();
        }
    }
}
