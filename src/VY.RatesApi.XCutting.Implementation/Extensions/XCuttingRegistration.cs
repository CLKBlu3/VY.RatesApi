using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VY.RatesApi.Business.Contracts.Domain;
using VY.RatesApi.XCutting.Contracts.Services;
using VY.RatesApi.XCutting.Implementation.Services;

namespace VY.RatesApi.XCutting.Implementation.Extensions
{
    public static class XCuttingRegistration
    {
        public static IServiceCollection AddXCuttingServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IProxy<Rate>, RatesProxy>();
            services.AddRedisClientsFromConfiguration(configuration);
            return services;
        }
    }
}
