using Microsoft.AspNetCore.Identity;

namespace cellShopSolution.Data.Entities
{
    public class User:IdentityUser<Guid>
    {
        public string FistName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay  { get; set; }
        public List<Cart> Carts { get; set; }
        public List<Order> Orders { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
