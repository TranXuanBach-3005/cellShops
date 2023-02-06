using cellShopSolution.Application.Repositorys.IRepository;
using cellShopSolution.Data.EF;
using cellShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace cellShopSolution.Application.Repositorys.Repository
{
    public class SlideRepository : GenericRepository<Slide>, ISlideRepository
    {
        public SlideRepository(cellShopDbContext context) : base(context)
        {
        }

        public async Task<List<Slide>> GetAllSlidesSort()
        {
            return await _dbSet.OrderBy(x => x.SortOrder).ToListAsync();
        }
    }
}
