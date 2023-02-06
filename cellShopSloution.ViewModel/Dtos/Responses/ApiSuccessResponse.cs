namespace cellShopSloution.ViewModel.Dtos.Responses
{
    public class ApiSuccessResponse<T>:ApiResponse<T>
    {
        public ApiSuccessResponse()
        {
            IsSuccessed = true;
        }
        public ApiSuccessResponse(T resultObj)
        {
            IsSuccessed = true;
            ResultObj = resultObj;
        }
    }
}
