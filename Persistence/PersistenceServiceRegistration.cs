using Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.DependencyInjection;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddDbContext<BaseDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("WheelDealDb")));

        serviceCollection.AddDbMigrationApplier(buildServices => buildServices.GetRequiredService<BaseDbContext>());

        serviceCollection.AddScoped<IAdditionalServiceRepository, AdditionalServiceRepository>();
        serviceCollection.AddScoped<IRentalRepository, RentalRepository>();
        serviceCollection.AddScoped<IRentalBranchRepository, RentalBranchRepository>();
        serviceCollection.AddScoped<IRentalsAdditionalServiceRepository, RentalsAdditionalServiceRepository>();

        return serviceCollection;
    }
}