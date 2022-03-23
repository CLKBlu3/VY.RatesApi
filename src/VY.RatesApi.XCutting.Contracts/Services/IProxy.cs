using System.Collections.Generic;
using System.Threading.Tasks;

namespace VY.RatesApi.XCutting.Contracts.Services
{
    public interface IProxy<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync();
    }
}