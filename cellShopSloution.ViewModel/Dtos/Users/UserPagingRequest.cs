using cellShopSolution.ViewModel.Dtos;

namespace cellShopSloution.ViewModel.Dtos.Users
{
    public class UserPagingRequest : PagingRequestBase
    {
        public string? Keyword { get; set; }
    }

}
