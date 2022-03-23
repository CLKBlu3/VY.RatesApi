using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VY.RatesApi.Dtos;
using VY.RatesApi.WebApi;
using VY.RatesApi.XCutting.Domain.OperationResult;
using Xunit;

namespace VY.RatesApi.IntegrationTest
{
    public class CreateIntegrationTest : IClassFixture<InMemoryDBFactory<Startup>>
    {
        private readonly InMemoryDBFactory<Startup> _factory;
        private readonly HttpClient _httpClient;

        public CreateIntegrationTest(InMemoryDBFactory<Startup> factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient();
        }

        [Fact]
        public async Task Create_ReturnsNoErrors()
        {
            var toInsertDto = new AccounteeDto()
            {
                Age = 23,
                DNI = "11111111H",
                Name = "Hans",
                Surname = "Solo",
                Accounts = new List<AccountDto>()
                {
                    new AccountDto()
                    {
                        Amount = 300,
                        Currency = "CAD",
                        Id = "1234-5678-91234-321"
                    },
                    new AccountDto()
                    {
                        Amount = 200,
                        Currency = "USD",
                        Id = "1234-5678-91234-322"
                    },
                }
            };

            var serilizedDto = JsonSerializer.Serialize(toInsertDto);
            var bodyContent = new StringContent(serilizedDto,
                                                Encoding.UTF8,
                                                "application/json");
            using (var response = await _httpClient.PostAsync("/Create", bodyContent))
            {
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                var deserializedContent = JsonSerializer.Deserialize<OperationResult>(content);
                Assert.False(deserializedContent.HasErrors());
            }
        }
    }
}
