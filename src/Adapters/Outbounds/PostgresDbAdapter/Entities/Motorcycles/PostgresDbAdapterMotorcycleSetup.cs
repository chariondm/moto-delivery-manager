namespace MotoDeliveryManager.Adapters.Outbounds.PostgresDbAdapter.Entities.Motorcycles
{
    public static class PostgresDbAdapterMotorcycleSetup
    {
        public static IServiceCollection AddPostgresDbAdapterMotorcycleRepository(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IDbConnectionFactory>(provider => new DbConnectionFactory(connectionString));

            services
                .AddScoped<IFilterMotorcyclesByLicensePlateRepository, MotorcycleRepository>()
                .AddScoped<IRegisterMotorcycleRepository, MotorcycleRepository>()
                .AddScoped<IUpdateMotorcycleLicensePlateRepository, MotorcycleRepository>();

            return services;
        }
    }
}
