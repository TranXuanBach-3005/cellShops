using cellShopSloution.ViewModel.Dtos.Responses;
using cellShopSloution.ViewModel.Dtos.Roles;

namespace cellShopSolution.ApiIntegration.Services.IService
{
    public interface IRoleClient
    {
        Task<ApiResponse<List<RoleViewModel>>> GetAllRoleAsync();
    }
}
