using cellShopSolution.Application.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cellShopSolution.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlidesController : ControllerBase
    {
       private readonly ISlideService _slideService;

        public SlidesController(ISlideService slideService)
        {
            _slideService = slideService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSiled()
        {
            var result = await _slideService.GetAllSlides();
            return Ok(result);
        }
    }
}
