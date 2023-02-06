using cellShopSloution.ViewModel.Dtos.Responses;
using cellShopSloution.ViewModel.Dtos.Users;
using cellShopSolution.Data.Entities;
using cellShopSolution.ViewModel.Dtos;

namespace cellShopSolution.Application.Services.IService
{
    public interface IUserService
    {
        Task<ApiResponse<string>> AuthencateAsync(LoginRequest loginRequest);
        Task<ApiResponse<bool>> RegisterAsync(RegisterRequest registerRequest);
        Task<ApiResponse<bool>> UpdateUserAsync(Guid id, UserUpdateResquest updateResquest);
        Task<ApiResponse<UserViewModel>> GetByIdUserAsync(Guid id);
        Task<ApiResponse<bool>> DeleteUserAsync(Guid id);
        Task<ApiResponse<PageResult<UserViewModel>>> GetListUserAsync(UserPagingRequest userPagingRequest);
        Task<ApiResponse<bool>> RoleUserAsync(Guid id, RoleUserRequest roleUserRequest);
    }
}
