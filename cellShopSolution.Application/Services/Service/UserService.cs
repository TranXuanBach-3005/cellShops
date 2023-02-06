using cellShopSloution.ViewModel.Dtos.Responses;
using cellShopSloution.ViewModel.Dtos.Users;
using cellShopSolution.Application.Services.IService;
using cellShopSolution.Data.Entities;
using cellShopSolution.ViewModel.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace cellShopSolution.Application.Services.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _config;
        public UserService(UserManager<User> userManager, SignInManager<User> signInManager,
                           IConfiguration config, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _roleManager = roleManager;
        }

        public async Task<ApiResponse<string>> AuthencateAsync(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByNameAsync(loginRequest.UserName);
            if (user == null) return new ApiErrorResponse<string>("Tài khoản không tồn tại");
            var result = await _signInManager.PasswordSignInAsync(user, loginRequest.Password, loginRequest.RememberMe, true);
            if (!result.Succeeded)
            {
                return new ApiErrorResponse<string>("Đăng nhập không thành công");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FistName),
                new Claim(ClaimTypes.Role, string.Join(";", roles)),
                new Claim(ClaimTypes.Name, loginRequest.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            return new ApiSuccessResponse<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public async Task<ApiResponse<bool>> DeleteUserAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResponse<bool>("Tài khoản không tồn tại");
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return new ApiSuccessResponse<bool>();
            }

            return new ApiErrorResponse<bool>("Xóa thất bại");
        }

        public async Task<ApiResponse<UserViewModel>> GetByIdUserAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResponse<UserViewModel>("Tài khoản không tồn tại");
            }
            var userViewModel = new UserViewModel()
            {
                Id = user.Id,
                LastName = user.LastName,
                FirstName = user.FistName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Birthday = user.BirthDay
            };
            return new ApiSuccessResponse<UserViewModel>(userViewModel);
        }

        public async Task<ApiResponse<PageResult<UserViewModel>>> GetListUserAsync(UserPagingRequest userPagingRequest)
        {
            var userQuery = _userManager.Users;
            if (!string.IsNullOrEmpty(userPagingRequest.Keyword))
            {
                userQuery = userQuery.Where(x => x.UserName.Contains(userPagingRequest.Keyword)
                                            || x.PhoneNumber.Contains(userPagingRequest.Keyword));
            }
            int totalRow = await userQuery.CountAsync();
            var userData = await userQuery.Skip((userPagingRequest.PageIndex - 1) * userPagingRequest.PageSize)
                           .Take(userPagingRequest.PageSize)
                           .Select(x => new UserViewModel()
                           {
                               Id = x.Id,
                               UserName = x.UserName,
                               PhoneNumber = x.PhoneNumber,
                               Email = x.Email,
                               FirstName = x.FistName,
                               LastName = x.LastName

                           }).ToListAsync();
            var pageResult = new PageResult<UserViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = userPagingRequest.PageIndex,
                PageSize = userPagingRequest.PageSize,
                Items = userData
            };
            return new ApiSuccessResponse<PageResult<UserViewModel>>(pageResult);
        }

        public async Task<ApiResponse<bool>> RegisterAsync(RegisterRequest registerRequest)
        {
            var user = await _userManager.FindByNameAsync(registerRequest.UserName);
            if (user != null)
            {
                return new ApiErrorResponse<bool>("Tên đăng nhập đã tồn tại");
            }
            if (await _userManager.FindByEmailAsync(registerRequest.Email) != null)
            {
                return new ApiErrorResponse<bool>("Email đã tồn tại");
            }
            user = new User()
            {
                FistName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
                BirthDay = registerRequest.BirthDay,
                Email = registerRequest.Email,
                UserName = registerRequest.UserName,
                PhoneNumber = registerRequest.Phone

            };
            var result = await _userManager.CreateAsync(user, registerRequest.Password);
            if (result.Succeeded)
            {
                return new ApiSuccessResponse<bool>();
            }
            return new ApiErrorResponse<bool>("Đăng ký thất bại");
        }

        public async Task<ApiResponse<bool>> RoleUserAsync(Guid id, RoleUserRequest roleUserRequest)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResponse<bool>("Tài khoản không tồn tại");
            }
            var removedRoles = roleUserRequest.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();
            foreach (var roleName in removedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == true)
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }
            await _userManager.RemoveFromRolesAsync(user, removedRoles);

            var addedRoles = roleUserRequest.Roles.Where(x => x.Selected).Select(x => x.Name).ToList();
            foreach (var roleName in addedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == false)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }

            return new ApiSuccessResponse<bool>();

        }

        public async Task<ApiResponse<bool>> UpdateUserAsync(Guid id, UserUpdateResquest updateResquest)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == updateResquest.Email && x.Id != id))
            {
                return new ApiErrorResponse<bool>("Email đã tồn tại");
            }
            var user = await _userManager.FindByIdAsync(id.ToString());
            user.BirthDay = updateResquest.BirthDay;
            user.Email = updateResquest.Email;
            user.FistName = updateResquest.FirstName;
            user.LastName = updateResquest.LastName;
            user.PhoneNumber = updateResquest.PhoneNumber;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ApiSuccessResponse<bool>();
            }
            return new ApiErrorResponse<bool>("Cập nhật thất bại");
        }
    }
}
