using Adapters.Outbounds.PostgresDbAdapter.Infrastructure.ConnectionFactory;
using Adapters.Outbounds.PostgresDbAdapter.Infrastructure.TypeHandlers;
using Adapters.Outbounds.PostgresDbAdapter.RepositoryImplementations;

using Core.Application.UseCases.FilterMotorcyclesByLicensePlate.Outbounds;
using Core.Application.UseCases.RegisterDeliveryDriver.Outbounds;
using Core.Application.UseCases.RegisterMotorcycle.Outbounds;
using Core.Application.UseCases.UpdateMotorcycleLicensePlate.Outbounds;

using Dapper;

using Microsoft.Extensions.DependencyInjection;

namespace Adapters.Outbounds.PostgresDbAdapter;

public static class PostgresDbAdapterSetup
{
    public static IServiceCollection AddPostgresDbAdapter(this IServiceCollection services, string connectionString)
    {
        services
            .AddPostgresDbAdapterMotorcycleRepository(connectionString)
            .AddPostgresDbAdapterDeliveryDriverRepository(connectionString);

        return services;
    }

    public static IServiceCollection AddPostgresDbAdapterMotorcycleRepository(this IServiceCollection services, string connectionString)
    {
        services
            .AddScoped<IFilterMotorcyclesByLicensePlateRepository, MotorcycleRepository>()
            .AddScoped<IRegisterMotorcycleRepository, MotorcycleRepository>()
            .AddScoped<IUpdateMotorcycleLicensePlateRepository, MotorcycleRepository>();

        return services;
    }

    public static IServiceCollection AddPostgresDbAdapterDeliveryDriverRepository(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IDbConnectionFactory>(provider => new DbConnectionFactory(connectionString));

        services.AddScoped<IRegisterDeliveryDriverRepository, DeliveryDriverRepository>();

        return services;
    }

    /// <summary>
    /// Adds the Dapper type handlers for the Postgres database adapter.
    /// </summary>
    /// <param name="services">The service collection to add the Dapper type handlers.</param>
    /// <returns>The modified service collection.</returns>
    /// <remarks>
    /// This method adds the Dapper type handlers for the Postgres database adapter. The type handlers are used to
    /// convert the database types to the corresponding .NET types when reading from the database and vice versa when
    /// writing to the database.
    /// </remarks>
    public static IServiceCollection AddPostgresDbAdapterDapperTypeHandlers(this IServiceCollection services)
    {
        SqlMapper.AddTypeHandler(new DapperSqlDateOnlyTypeHandler());

        return services;
    }

}
