using cellShopSloution.ViewModel.Dtos.Slides;
using cellShopSolution.Application.Services.IService;
using cellShopSolution.Application.UnitOfworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cellShopSolution.Application.Services.Service
{
    public class SlideService : ISlideService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SlideService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<SlideViewModel>> GetAllSlides()
        {
            var slides = await _unitOfWork.SlideRepository.GetAllSlidesSort();
            return slides.Select(x => new SlideViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Url = x.Url,
                Image = x.Image
            }).ToList();
        }
    }
}
