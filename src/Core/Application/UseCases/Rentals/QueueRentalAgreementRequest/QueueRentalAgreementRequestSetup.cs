namespace MotoDeliveryManager.Core.Application.UseCases.Rentals.QueueRentalAgreementRequest;

public static class QueueRentalAgreementRequestSetup
{
    public static IServiceCollection AddQueueRentalAgreementRequestUseCase(this IServiceCollection services)
    {
        services
            .AddScoped<IValidator<QueueRentalAgreementRequestInbound>, QueueRentalAgreementRequestInboundValidator>();

        services
            .AddKeyedScoped<IQueueRentalAgreementRequestUseCase, QueueRentalAgreementRequestUseCase>(UseCaseType.UseCase)
            .AddKeyedScoped<IQueueRentalAgreementRequestUseCase, QueueRentalAgreementRequestValidation>(UseCaseType.Validation);

        return services;
    }
}
