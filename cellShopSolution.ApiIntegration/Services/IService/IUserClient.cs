using cellShopSloution.ViewModel.Dtos.Responses;
using cellShopSloution.ViewModel.Dtos.Users;
using cellShopSolution.ViewModel.Dtos;

namespace cellShopSolution.ApiIntegration.Services.IService
{
    public interface IUserClient
    {
        Task<ApiResponse<string>> AuthenticateAsync(LoginRequest loginRequest);
        Task<ApiResponse<PageResult<UserViewModel>>> GetUsersPagingAsync(UserPagingRequest userPaging);
        Task<ApiResponse<bool>> RegisterUserAsync(RegisterRequest registerRequest);
        Task<ApiResponse<bool>> UpdateUserAsync(Guid id, UserUpdateResquest updateResquest);
        Task<ApiResponse<UserViewModel>> GetByIdUserAsync(Guid id);
        Task<ApiResponse<bool>> DeleteUserAsync(Guid id);
        Task<ApiResponse<bool>> RoleUserAsync(Guid id, RoleUserRequest roleUserRequest);
    }
}
