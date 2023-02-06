using cellShopSolution.Data.Entities;

namespace cellShopSolution.Application.Repositorys.IRepository
{
    public interface ICategoryRepository:IGenericRepository<Category>
    {
        Task<List<Category>> GetListCategory(string languageId);
        Task<Category> GetByCategory(string languageId, int categoryId);
    }
}
