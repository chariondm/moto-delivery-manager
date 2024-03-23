using MotoDeliveryManager.Core.Application.Common;
using MotoDeliveryManager.Core.Application.UseCases.RegisterDeliveryDriver.Inbounds;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace MotoDeliveryManager.Core.Application.UseCases.RegisterDeliveryDriver;

public static class RegisterDeliveryDriverSetup
{
    public static IServiceCollection AddRegisterDeliveryDriverUseCase(this IServiceCollection services)
    {
        services
            .AddScoped<IValidator<RegisterDeliveryDriverInbound>, RegisterDeliveryDriverInboundValidator>();

        services
            .AddKeyedScoped<IRegisterDeliveryDriverUseCase, RegisterDeliveryDriverUseCase>(UseCaseType.UseCase)
            .AddKeyedScoped<IRegisterDeliveryDriverUseCase, RegisterDeliveryDriverValidation>(UseCaseType.Validation);

        return services;
    }
}
