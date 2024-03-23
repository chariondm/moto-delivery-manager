namespace MotoDeliveryManager.Core.Application.UseCases.Motorcycles.UpdateMotorcycleLicensePlate;

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
