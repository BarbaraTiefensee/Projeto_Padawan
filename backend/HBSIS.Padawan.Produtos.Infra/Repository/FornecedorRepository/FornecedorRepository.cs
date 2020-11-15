using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Infra.Context;
using HBSIS.Padawan.Produtos.Infra.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Infra.Repository.FornecedorRepository
{
    public class FornecedorRepository : GenericRepository<FornecedorEntity>, IFornecedorRepository
    {
        private readonly MainContext _context;
        public FornecedorRepository(MainContext context) : base(context)
        {
        }

        public async Task<bool> ExisteFornecedorComEsseCnpj(string cnpj, int id)
        {
            return await _dbSet.AnyAsync(x => x.CNPJ == cnpj && x.Id != id && x.Deletado != true);
        }

        public async Task<bool> VerificarSeFornecedorJaExiste(string cnpj)
        {
            return await _dbSet.AnyAsync(x => x.CNPJ == cnpj && x.Deletado == false);
        }

    }
};
