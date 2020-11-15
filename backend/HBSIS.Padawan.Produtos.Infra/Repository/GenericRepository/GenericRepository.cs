using HBSIS.Padawan.Produtos.Domain.Entities;
using HBSIS.Padawan.Produtos.Domain.Interfaces;
using HBSIS.Padawan.Produtos.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HBSIS.Padawan.Produtos.Infra.Repository.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly MainContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(MainContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        } 

        public  async Task<TEntity> GetById(int id)
        {
            return await Query()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id && !e.Deletado);
        }

        public async Task Create(TEntity entity)
        { 
                await _dbSet.AddAsync(entity);
                await _dbContext.SaveChangesAsync(); 
        }

        public async Task Update(TEntity entity)
        {
                _dbContext.Update(entity);
                await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<TEntity> Query() => _dbSet.AsNoTracking();

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Task.FromResult(Query().Where(e => !e.Deletado));
        }
    }
}