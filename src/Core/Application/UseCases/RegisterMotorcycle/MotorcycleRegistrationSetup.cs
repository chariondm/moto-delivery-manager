using MotoDeliveryManager.Core.Application.Common;
using MotoDeliveryManager.Core.Application.UseCases.RegisterMotorcycle.Inbounds;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace MotoDeliveryManager.Core.Application.UseCases.RegisterMotorcycle;

public static class MotorcycleRegistrationSetup
{
    public static IServiceCollection AddMotorcycleRegistrationUseCase(this IServiceCollection services)
    {
        services
            .AddScoped<IValidator<MotorcycleRegistrationInbound>, MotorcycleRegistrationInboundValidator>();

        services
            .AddKeyedScoped<IMotorcycleRegistrationUseCase, MotorcycleRegistrationUseCase>(UseCaseType.UseCase)
            .AddKeyedScoped<IMotorcycleRegistrationUseCase, MotorcycleRegistrationValidation>(UseCaseType.Validation);

        return services;
    }
}
