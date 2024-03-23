namespace MotoDeliveryManager.Core.Application.UseCases.Motorcycles.RegisterMotorcycle;

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
