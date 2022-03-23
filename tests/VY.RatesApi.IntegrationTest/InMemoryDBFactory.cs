using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using VY.RatesApi.Infrastructure.Implementation.Context;

namespace VY.RatesApi.IntegrationTest
{
    public class InMemoryDBFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureServices(services =>
            {
                var db = services.SingleOrDefault(c => c.ServiceType == typeof(DbContextOptions<CurrencyDbContext>));
                services.Remove(db);
                services.AddDbContext<CurrencyDbContext>(c =>
                {
                    c.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
                    c.UseInMemoryDatabase("InMemoryDBForTestingPurposes");
                });
            });
        }
    }
}
