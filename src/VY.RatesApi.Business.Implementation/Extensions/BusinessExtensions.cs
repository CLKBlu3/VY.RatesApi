using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VY.RatesApi.Business.Contracts.Services;
using VY.RatesApi.Business.Implementation.Mapping_Profiles;
using VY.RatesApi.Business.Implementation.Services;
using VY.RatesApi.Infrastructure.Implementation.Extensions;
using VY.RatesApi.XCutting.Implementation.Extensions;

namespace VY.RatesApi.Business.Implementation.Extensions
{
    public static class BusinessExtensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services, 
                                                             IConfiguration configuration)
        {
            services.AddInfrastructureServices(configuration);
            services.AddXCuttingServices();

            services.AddAutoMapper(typeof(AccounteeProfile),
                                   typeof(AccountProfile));

            services.AddTransient<IRatesService, RatesService>();
            services.AddTransient<IAccountService, AccountService>();
            return services;
        }
    }
}
