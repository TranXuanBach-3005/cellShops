using Microsoft.AspNetCore.Identity;

namespace cellShopSolution.Data.Entities
{
    public class Role:IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
