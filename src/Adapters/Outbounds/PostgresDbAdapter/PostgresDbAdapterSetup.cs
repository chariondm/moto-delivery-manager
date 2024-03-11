using Adapters.Outbounds.PostgresDbAdapter.RepositoryImplementations;
using Core.Application.UseCases.RegisterMotorcycle.Outbounds;

using Microsoft.Extensions.DependencyInjection;

namespace Adapters.Outbounds.PostgresDbAdapter;

public static class PostgresDbAdapterSetup
{
    public static IServiceCollection AddMotorcycleRepositoryRepository(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IMotorcycleRepository>(provider => new MotorcycleRepository(connectionString));

        return services;
    }
}
