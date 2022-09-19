using Microsoft.Extensions.DependencyInjection;
using TFG.Assessment.EFCore.Repository;
using TFG.Assessment.Domain.Interfaces;
using TFG.Assessment.Domain.Interfaces.Repository;

namespace TFG.Assessment.EFCore
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddEffCollections(this IServiceCollection services)
        {
            services.AddDbContext<CustomersContext>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
