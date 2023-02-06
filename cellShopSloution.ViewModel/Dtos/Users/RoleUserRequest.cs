namespace cellShopSloution.ViewModel.Dtos.Users
{
    public class RoleUserRequest
    {
        public Guid Id { get; set; }
        public List<SelectItem> Roles { get; set; } = new List<SelectItem>();
    }
}
