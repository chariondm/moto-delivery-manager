namespace MotoDeliveryManager.Adapters.Outbounds.PostgresDbAdapter.Entities.Rentals
{
    public static class PostgresDbAdapterRentalPlanSetup
    {
        public static IServiceCollection AddPostgresDbAdapterRentalPlanRepository(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IDbConnectionFactory>(provider => new DbConnectionFactory(connectionString));

            services
                .AddScoped<IListRentalPlansRepository, RentalPlanRepository>();

            return services;
        }
    }
}
