using Adapters.Outbounds.PostgresDbAdapter.RepositoryImplementations;
using Core.Application.UseCases.FilterMotorcyclesByLicensePlate.Outbounds;
using Core.Application.UseCases.RegisterMotorcycle.Outbounds;
using Core.Application.UseCases.UpdateMotorcycleLicensePlate.Outbounds;

using Microsoft.Extensions.DependencyInjection;

namespace Adapters.Outbounds.PostgresDbAdapter;

public static class PostgresDbAdapterSetup
{
    public static IServiceCollection AddPostgresDbAdapter(this IServiceCollection services, string connectionString)
    {
        services
            .AddScoped<IFilterMotorcyclesByLicensePlateRepository>(provider => new MotorcycleRepository(connectionString))
            .AddScoped<IRegisterMotorcycleRepository>(provider => new MotorcycleRepository(connectionString))
            .AddScoped<IUpdateMotorcycleLicensePlateRepository>(provider => new MotorcycleRepository(connectionString));

        return services;
    }
}
