using System.Collections.Generic;
using System.Threading.Tasks;
using VY.RatesApi.Infrastructure.Contracts.Repostiories;
using VY.RatesApi.Infrastructure.Implementation.Context;

namespace VY.RatesApi.Infrastructure.Implementation.Repositories
{
    public class GenericRepository : IRepository
    {
        private readonly CurrencyDbContext _context;
        public GenericRepository(CurrencyDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
