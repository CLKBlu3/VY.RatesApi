using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VY.RatesApi.Infrastructure.Contracts.Repostiories;
using VY.RatesApi.Infrastructure.Implementation.Context;
using VY.RatesApi.Infrastructure.Implementation.Repositories;

namespace VY.RatesApi.Infrastructure.Implementation.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, 
                                                                   IConfiguration configuration)
        {
            services.AddDbContext<CurrencyDbContext>(opt =>
            {
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
                opt.UseSqlServer(configuration.GetConnectionString("CurrencyDb"));
            });

            services.AddTransient<IRepository, GenericRepository>();

            return services;
        }
    }
}
