using MotoDeliveryManager.Core.Application.Common;
using MotoDeliveryManager.Core.Application.UseCases.FilterMotorcyclesByLicensePlate.Inbounds;

using Microsoft.Extensions.DependencyInjection;

namespace MotoDeliveryManager.Core.Application.UseCases.FilterMotorcyclesByLicensePlate;

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
