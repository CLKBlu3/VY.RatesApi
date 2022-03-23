using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VY.RatesApi.Business.Contracts.Domain;
using VY.RatesApi.Business.Implementation.Services;
using VY.RatesApi.Dtos;
using VY.RatesApi.XCutting.Contracts.Services;
using Xunit;

namespace VY.RatesApi.UnitTests.Business
{
    public class RatesServiceTests
    {
        [Fact]
        public async Task ConverToEur_IsSuccesfulAsync()
        {
            Mock<IProxy<Rate>> mockedRates = new Mock<IProxy<Rate>>();
            Rate moqdRate = new Rate()
            {
                From = "USD",
                To = "EUR",
                Ratio = "2.0"
            };
            RatesService rs = new RatesService(mockedRates.Object);
            IEnumerable<Rate> returnedRates = new List<Rate>() { moqdRate };
            mockedRates.Setup(x => x.GetAsync()).Returns(Task.FromResult(returnedRates));
            IEnumerable<AccountDto> accounts = new List<AccountDto>() {
                new AccountDto()
                {
                    Amount = 300,
                    Currency = "USD",
                    Id = "1234"
                },
                new AccountDto()
                {
                    Amount = 120,
                    Currency = "USD",
                    Id = "4321"
                }
            };
            var res = await rs.ConvertToEurAsync(accounts);

            Assert.False(res.HasExceptions());
            Assert.False(res.HasErrors());
            Assert.Equal(600, res.Result.First().Amount);
            Assert.Equal(240, res.Result.Last().Amount);
        }
    }
}
