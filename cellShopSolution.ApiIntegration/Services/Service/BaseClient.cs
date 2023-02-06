using cellShopSolution.Utilities.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace cellShopSolution.ApiIntegration.Services.Service
{
    public class BaseClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        protected BaseClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        protected async Task<T> GetAllAsync<T>(string url)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString(SystemConstant.AppSettings.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstant.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                T myDeserializedObjList = (T)JsonConvert.DeserializeObject(result, typeof(T));
                return myDeserializedObjList;

            }
            return JsonConvert.DeserializeObject<T>(result);


        }
        public async Task<List<T>> GetListAsync<T>(string url)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString(SystemConstant.AppSettings.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstant.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var myDeserializedObjList = (List<T>)JsonConvert.DeserializeObject(result, typeof(List<T>));
                return myDeserializedObjList;

            }
            return JsonConvert.DeserializeObject<List<T>>(result);


        }
    }
}
