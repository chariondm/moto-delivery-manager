using Core.Application.Common;
using Core.Application.UseCases.FilterMotorcyclesByLicensePlate.Inbounds;

using Microsoft.Extensions.DependencyInjection;

namespace Core.Application.UseCases.FilterMotorcyclesByLicensePlate;

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
