using cellShopSloution.ViewModel.Dtos.Roles;

namespace cellShopSolution.Application.Services.IService
{
    public interface IRoleService
    {
        Task<List<RoleViewModel>> GetAllRoleAsync();
    }
}
