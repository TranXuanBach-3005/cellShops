using cellShopSloution.ViewModel.Dtos;

namespace cellShopSolution.ViewModel.Dtos
{
    public class PageResult<T>:PageResultBase
    {
        public List<T> Items { set; get; }
    }
}
