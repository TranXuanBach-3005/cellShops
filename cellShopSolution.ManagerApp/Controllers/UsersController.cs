using cellShopSloution.ViewModel.Dtos;
using cellShopSloution.ViewModel.Dtos.Users;
using cellShopSolution.ApiIntegration.Services.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace cellShopSolution.ManagerApp.Controllers
{
    public class UsersController : BasesController
    {
        private readonly IUserClient _userClient;
        private readonly IRoleClient _roleClient;
        private readonly IConfiguration _configuration;
        public UsersController(IUserClient userClient,
            IConfiguration configuration, IRoleClient roleClient)
        {
            _userClient = userClient;
            _configuration = configuration;
            _roleClient = roleClient;
        }

        public async Task<IActionResult> Index(string? keyword, int pageIndex = 1, int pageSize = 1)
        {
            var userRequest = new UserPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var userData = await _userClient.GetUsersPagingAsync(userRequest);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMessage = TempData["result"];
            }
            return View(userData.ResultObj);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Login", "Users");
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _userClient.GetByIdUserAsync(id);
            return View(result.ResultObj);
        }
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            return View(new UserDeleteRequest()
            {
                Id = id
            });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UserDeleteRequest deleteRequest)
        {
            if (!ModelState.IsValid)
                return View();
            var result = await _userClient.DeleteUserAsync(deleteRequest.Id);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Xóa thành công";
                return RedirectToAction("Index");
            }
            return View(deleteRequest);
        }
        //
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
                return View();
            var result = await _userClient.RegisterUserAsync(registerRequest);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", result.Message);
            return View(registerRequest);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _userClient.GetByIdUserAsync(id);
            if (result.IsSuccessed)
            {
                var user = result.ResultObj;
                var userUpdate = new UserUpdateResquest()
                {
                    Id = user.Id,
                    BirthDay = user.Birthday,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email
                };
                return View(userUpdate);
            }
            return RedirectToAction("Error", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateResquest updateResquest)
        {
            if (!ModelState.IsValid)
                return View();
            var result = await _userClient.UpdateUserAsync(updateResquest.Id, updateResquest);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Cập nhật thành công";
                return RedirectToAction("Index");
            }
            return View(updateResquest);
        }
        [HttpGet]
        public async Task<IActionResult> RoleUser(Guid id)
        {
            var rolesUserRequest = await GetRoleUserRequest(id);
            return View(rolesUserRequest);
        }
        [HttpPost]
        public async Task<IActionResult> RoleUser(RoleUserRequest roleUserRequest)
        {
            if (!ModelState.IsValid)
                return View();
            var result = await _userClient.RoleUserAsync(roleUserRequest.Id, roleUserRequest);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Gán quyền thành công";
                return RedirectToAction("Index");
            }
            var rolesUserRequest = await GetRoleUserRequest(roleUserRequest.Id);
            return View(rolesUserRequest);
        }
        private async Task<RoleUserRequest> GetRoleUserRequest(Guid id)
        {
            var userResult = await _userClient.GetByIdUserAsync(id);
            var roleObj = await _roleClient.GetAllRoleAsync();
            var rolesUserRequest = new RoleUserRequest();
            foreach (var role in roleObj.ResultObj)
            {
                rolesUserRequest.Roles.Add(new SelectItem()
                {
                    Id = role.Id.ToString(),
                    Name = role.Name,
                    Selected = userResult.ResultObj.Roles.Contains(role.Name)

                });
            }
            return rolesUserRequest;
        }
    }
}
