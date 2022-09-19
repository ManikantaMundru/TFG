using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TFG.Assessment.Domain.Entities;
using TFG.Assessment.EFCore;

namespace TFG.Assesment.Api.Tests
{
    public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<CustomersContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<CustomersContext>(options =>
                {
                    options.UseInMemoryDatabase("Customers");
                });

            });
        }
    }
}
