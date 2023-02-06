namespace cellShopSolution.ViewModel.Dtos.Products
{
    public class GetProductPagingRequest: PagingRequestBase
    {
        public string? Keyword { get; set; }
        public string LanguageId { get; set; }
        public int? CategoryId { get; set; }
    }
}
