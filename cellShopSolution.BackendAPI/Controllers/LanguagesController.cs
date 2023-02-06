using cellShopSolution.Application.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace cellShopSolution.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly ILanguageService _languageService;

        public LanguagesController(ILanguageService languageService)
        {
            _languageService = languageService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLanguage()
        {
            var result = await _languageService.GetAllLanguageAsync();
            return Ok(result);
        }
    }
}
