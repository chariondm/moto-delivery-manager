namespace MotoDeliveryManager.Adapters.Outbounds.PostgresDbAdapter.Entities.DeliveryDrivers
{
    public static class PostgresDbAdapterDeliveryDriverSetup
    {

        public static IServiceCollection AddPostgresDbAdapterDeliveryDriverRepository(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IDbConnectionFactory>(provider => new DbConnectionFactory(connectionString));

            services
                .AddScoped<IProcessDriverLicensePhotoUploadRepository, DeliveryDriverRepository>()
                .AddScoped<IRegisterDeliveryDriverRepository, DeliveryDriverRepository>();

            return services;
        }

        public static IServiceCollection AddSingletonAddPostgresDbAdapterDeliveryDriverRepository(
            this IServiceCollection services,
            string connectionString)
        {
            services.AddSingleton<IDbConnectionFactory>(provider => new DbConnectionFactory(connectionString));

            services
                .AddSingleton<IProcessDriverLicensePhotoUploadRepository, DeliveryDriverRepository>()
                .AddSingleton<IRegisterDeliveryDriverRepository, DeliveryDriverRepository>();

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

}
