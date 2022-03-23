using System.Collections.Generic;
using System.Threading.Tasks;
using VY.RatesApi.Dtos;
using VY.RatesApi.XCutting.Domain.OperationResult;

namespace VY.RatesApi.Business.Contracts.Services
{
    public interface IRatesService
    {
        public Task<OperationResult<IEnumerable<AccountDto>>> ConvertToEurAsync(IEnumerable<AccountDto> accountDto);
    }
}