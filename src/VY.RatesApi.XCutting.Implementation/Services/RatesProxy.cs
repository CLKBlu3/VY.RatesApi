using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using VY.RatesApi.Business.Contracts.Domain;
using VY.RatesApi.XCutting.Contracts.Services;

namespace VY.RatesApi.XCutting.Implementation.Services
{
    public class RatesProxy : IProxy<Rate>
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public RatesProxy(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Rate>> GetAsync()
        {
            var res = new List<Rate>();
            using (var request = new HttpRequestMessage(HttpMethod.Get, _configuration["ratesUri"]))
            {
                using (var response = await _client.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        res = JsonSerializer.Deserialize<List<Rate>>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
            return res;
        }
    }
}
