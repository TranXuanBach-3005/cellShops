using cellShopSolution.Application.Repositorys.IRepository;
using cellShopSolution.Data.EF;
using cellShopSolution.Data.Entities;

namespace cellShopSolution.Application.Repositorys.Repository
{
    public class LanguageRepository : GenericRepository<Language>, ILanguageRepository
    {
        public LanguageRepository(cellShopDbContext context) : base(context)
        {
        }
    }
}
