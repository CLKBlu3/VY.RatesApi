using System.Threading.Tasks;
using VY.RatesApi.Dtos;
using VY.RatesApi.XCutting.Domain.OperationResult;

namespace VY.RatesApi.Business.Contracts.Services
{
    public interface IAccountService
    {
        public Task<OperationResult> Create(AccounteeDto accounteeDto);
    }
}