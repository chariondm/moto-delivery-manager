using MotoDeliveryManager.Core.Application.Common;
using MotoDeliveryManager.Core.Application.UseCases.UpdateMotorcycleLicensePlate.Inbounds;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace MotoDeliveryManager.Core.Application.UseCases.UpdateMotorcycleLicensePlate;

public static class UpdateMotorcycleLicensePlateSetup
{
    public static IServiceCollection AddUpdateMotorcycleLicensePlateUseCase(this IServiceCollection services)
    {
        services
            .AddScoped<IValidator<UpdateMotorcycleLicensePlateInbound>, UpdateMotorcycleLicensePlateInboundValidator>();

        services
            .AddKeyedScoped<IUpdateMotorcycleLicensePlateUseCase, UpdateMotorcycleLicensePlateUseCase>(UseCaseType.UseCase)
            .AddKeyedScoped<IUpdateMotorcycleLicensePlateUseCase, UpdateMotorcycleLicensePlateValidation>(UseCaseType.Validation);

        return services;
    }
}
