namespace cellShopSloution.ViewModel.Dtos.Responses
{
    public class ApiResponse<T>
    {
        public bool IsSuccessed { get; set; }
        public string Message { get; set; }
        public T ResultObj { get; set; }
    }
}
