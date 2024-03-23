namespace MotoDeliveryManager.Core.Application.UseCases.DeliveryDrivers.RegisterDeliveryDriver;

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
