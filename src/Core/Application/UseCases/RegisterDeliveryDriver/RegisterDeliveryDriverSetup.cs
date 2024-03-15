using Core.Application.Common;
using Core.Application.UseCases.RegisterDeliveryDriver.Inbounds;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace Core.Application.UseCases.RegisterDeliveryDriver;

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
