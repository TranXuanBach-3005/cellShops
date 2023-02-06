
using cellShopSolution.Application.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cellShopSolution.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GellAllRole()
        {
            var result = await _roleService.GetAllRoleAsync();
            return Ok(result);
        }
    }
}
