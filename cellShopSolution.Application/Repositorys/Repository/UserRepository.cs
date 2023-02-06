using cellShopSolution.Application.Repositorys.IRepository;
using cellShopSolution.Data.EF;
using cellShopSolution.Data.Entities;

namespace cellShopSolution.Application.Repositorys.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(cellShopDbContext context) : base(context)
        {
        }
    }
}
