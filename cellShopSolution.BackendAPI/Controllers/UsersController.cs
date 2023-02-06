using cellShopSloution.ViewModel.Dtos.Users;
using cellShopSolution.Application.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cellShopSolution.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.AuthencateAsync(loginRequest);
            if (string.IsNullOrEmpty(result.ResultObj))
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut("{id}/roles")]
        [AllowAnonymous]
        public async Task<IActionResult> RolesUser(Guid id, [FromBody] RoleUserRequest roleUserRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.RoleUserAsync(id, roleUserRequest);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.RegisterAsync(registerRequest);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateUser(Guid id,[FromBody] UserUpdateResquest updateResquest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.UpdateUserAsync(id,updateResquest);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("paging")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllUserPaging([FromQuery]UserPagingRequest userPagingRequest)
        {
            var users = await _userService.GetListUserAsync(userPagingRequest);
            return Ok(users);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdUser(Guid id)
        {
            var users = await _userService.GetByIdUserAsync(id);
            return Ok(users);
        }
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _userService.DeleteUserAsync(id);
            return Ok(result);
        }
    }
}
