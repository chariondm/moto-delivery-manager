using Core.Application.Common;
using Core.Application.UseCases.RegisterMotorcycle.Inbounds;

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application.UseCases.RegisterMotorcycle;

public static class MotorcycleRegistrationSetup
{
    public static IServiceCollection AddMotorcycleRegistrationUseCase(this IServiceCollection services)
    {
        services
            .AddScoped<IValidator<MotorcycleRegistrationInbound>, MotorcycleRegistrationInboundValidator>();

        services
            .AddKeyedScoped<IMotorcycleRegistrationProcessor, MotorcycleRegistrationProcessor>(UseCaseType.UseCase)
            .AddKeyedScoped<IMotorcycleRegistrationProcessor, MotorcycleRegistrationValidation>(UseCaseType.Validation);

        return services;
    }
}
