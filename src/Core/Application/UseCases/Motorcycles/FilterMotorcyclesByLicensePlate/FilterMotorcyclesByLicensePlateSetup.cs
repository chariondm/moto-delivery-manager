namespace MotoDeliveryManager.Core.Application.UseCases.Motorcycles.FilterMotorcyclesByLicensePlate;

public static class FilterMotorcyclesByLicensePlateSetup
{
    public static IServiceCollection AddFilterMotorcyclesByLicensePlateUseCase(this IServiceCollection services)
    {

        services
            .AddKeyedScoped<IFilterMotorcyclesByLicensePlateUseCase, FilterMotorcyclesByLicensePlateUseCase>(UseCaseType.UseCase)
            .AddKeyedScoped<IFilterMotorcyclesByLicensePlateUseCase, FilterMotorcyclesByLicensePlateValidation>(UseCaseType.Validation);

        return services;
    }
}
