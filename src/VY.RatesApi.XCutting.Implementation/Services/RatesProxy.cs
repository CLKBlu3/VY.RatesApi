using ATC.RedisClient.Contracts.ServiceLibrary;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IRedisCacheClient _redisCacheClient;

        public RatesProxy(HttpClient client, 
                          IConfiguration configuration,
                          IRedisCacheClient redisCacheClient)
        {
            _client = client;
            _configuration = configuration;
            _redisCacheClient = redisCacheClient;
        }

        public async Task<IEnumerable<Rate>> GetAsync()
        {
            var ratesFromCache = await _redisCacheClient.GetAsync<IEnumerable<Rate>>("RatesValues");
            if(ratesFromCache.IsSuccessfulOperation && ratesFromCache.CacheValue.Any())
            {
                return ratesFromCache.CacheValue;
            }
            var res = new List<Rate>();
            using (var request = new HttpRequestMessage(HttpMethod.Get, _configuration["ratesUri"]))
            {
                using (var response = await _client.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        res = JsonSerializer.Deserialize<List<Rate>>(await response.Content.ReadAsStringAsync());
                        await _redisCacheClient.SetAsync<IEnumerable<Rate>>("RatesValues", res);
                    }
                }
            }
            return res;
        }
    }
}
