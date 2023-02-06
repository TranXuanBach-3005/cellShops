namespace cellShopSloution.ViewModel.Dtos.Users
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
    }
}
