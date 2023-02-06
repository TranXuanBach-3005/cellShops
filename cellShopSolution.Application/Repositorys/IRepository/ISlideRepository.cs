using cellShopSloution.ViewModel.Dtos.Slides;
using cellShopSolution.Data.Entities;

namespace cellShopSolution.Application.Repositorys.IRepository
{
    public interface ISlideRepository:IGenericRepository<Slide>
    {
        Task<List<Slide>> GetAllSlidesSort();
    }
}
