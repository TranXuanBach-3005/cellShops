using cellShopSolution.Application.Repositorys.IRepository;
using cellShopSolution.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace cellShopSolution.Application.Repositorys.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected DbSet<TEntity> _dbSet;

        public GenericRepository(cellShopDbContext context)
        {
            _dbSet = context.Set<TEntity>();
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task CreateRangeAsync(List<TEntity> entity)
        {
            await _dbSet.AddRangeAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }
    }
}
