using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Product.API.Models;
using Product.ApiServices.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Products.Client.Services
{
    public class PieApiService : IPieApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PieApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {

            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<IEnumerable<Pie>> GetPies()
        {
            var httpClient = _httpClientFactory.CreateClient("ProductAPIClient");

            var request = new HttpRequestMessage(HttpMethod.Get, "/api/pies");

            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var pieList = JsonConvert.DeserializeObject<List<Pie>>(content);


            return pieList;
        }

        public Task DeletePie(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Pie> GetPie(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Pie> CreatePie(Pie pie)
        {
            throw new System.NotImplementedException();
        }

        public Task<Pie> UpdatePie(Pie pie)
        {
            throw new System.NotImplementedException();
        }


    }
}
