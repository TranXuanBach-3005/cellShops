namespace cellShopSolution.Application.Repositorys.IRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int Id);
        Task CreateAsync(TEntity entity);
        Task CreateRangeAsync(List<TEntity> entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
