using cellShopSloution.ViewModel.Dtos.Responses;
using cellShopSloution.ViewModel.Dtos.Users;
using cellShopSolution.ApiIntegration.Services.IService;
using cellShopSolution.ViewModel.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace cellShopSolution.ApiIntegration.Services.Service
{
    public class UserClient : IUserClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
                          IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<string>> AuthenticateAsync(LoginRequest loginRequest)
        {
            var jsonData = JsonConvert.SerializeObject(loginRequest);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var response = await client.PostAsync("/api/Users/authenticate", content);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResponse<string>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiErrorResponse<string>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiResponse<bool>> DeleteUserAsync(Guid id)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.DeleteAsync($"/api/Users/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResponse<bool>>(result);
            }
            return JsonConvert.DeserializeObject<ApiErrorResponse<bool>>(result);

        }

        public async Task<ApiResponse<UserViewModel>> GetByIdUserAsync(Guid id)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync($"/api/Users/{id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResponse<UserViewModel>>(result);
            }
            return JsonConvert.DeserializeObject<ApiErrorResponse<UserViewModel>>(result);
        }

        public async Task<ApiResponse<PageResult<UserViewModel>>> GetUsersPagingAsync(UserPagingRequest userPaging)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync($"/api/Users/paging?pageIndex=" +
                $"{userPaging.PageIndex}&pageSize={userPaging.PageSize}&keyword={userPaging.Keyword}");
            var result = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<ApiSuccessResponse<PageResult<UserViewModel>>>(result);
            return users;
        }

        public async Task<ApiResponse<bool>> RegisterUserAsync(RegisterRequest registerRequest)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var jonData = JsonConvert.SerializeObject(registerRequest);
            var content = new StringContent(jonData, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"/api/Users/register", content);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResponse<bool>>(result);
            }
            return JsonConvert.DeserializeObject<ApiErrorResponse<bool>>(result);
        }

        public async Task<ApiResponse<bool>> RoleUserAsync(Guid id, RoleUserRequest roleUserRequest)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var jonData = JsonConvert.SerializeObject(roleUserRequest);
            var content = new StringContent(jonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"/api/Users/{id}/roles", content);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResponse<bool>>(result);
            }
            return JsonConvert.DeserializeObject<ApiErrorResponse<bool>>(result);

        }

        public async Task<ApiResponse<bool>> UpdateUserAsync(Guid id, UserUpdateResquest updateResquest)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var jonData = JsonConvert.SerializeObject(updateResquest);
            var content = new StringContent(jonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"/api/Users/{id}", content);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResponse<bool>>(result);
            }
            return JsonConvert.DeserializeObject<ApiErrorResponse<bool>>(result);
        }
    }
}
