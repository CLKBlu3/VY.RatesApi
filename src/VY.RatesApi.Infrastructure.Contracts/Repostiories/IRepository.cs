using System.Collections.Generic;
using System.Threading.Tasks;

namespace VY.RatesApi.Infrastructure.Contracts.Repostiories
{
    public interface IRepository
    {
        public Task AddAsync<T>(IEnumerable<T> entities) where T : class;
        public Task SaveChangesAsync();
    }
}