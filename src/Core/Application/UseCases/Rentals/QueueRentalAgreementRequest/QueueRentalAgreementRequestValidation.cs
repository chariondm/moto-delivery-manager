namespace MotoDeliveryManager.Core.Application.UseCases.Rentals.QueueRentalAgreementRequest;

public sealed class QueueRentalAgreementRequestValidation(IServiceProvider serviceProvider)
    : IQueueRentalAgreementRequestUseCase
{
    private IQueueRentalAgreementRequestOutcomeHandler? _outcomeHandler;

    private readonly IQueueRentalAgreementRequestDeliveryDriverRepository _repository = serviceProvider
        .GetRequiredService<IQueueRentalAgreementRequestDeliveryDriverRepository>();

    private readonly IQueueRentalAgreementRequestUseCase _useCase = serviceProvider
        .GetRequiredKeyedService<IQueueRentalAgreementRequestUseCase>(UseCaseType.UseCase);

    private readonly IValidator<QueueRentalAgreementRequestInbound> _validator = serviceProvider
        .GetRequiredService<IValidator<QueueRentalAgreementRequestInbound>>();

    public async Task ExecuteAsync(QueueRentalAgreementRequestInbound inbound, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(inbound);

        if (!validationResult.IsValid)
        {
            _outcomeHandler!.RentalAgreementRequestNotValid(validationResult.ToDictionary());
            return;
        }

        var deliveryDriverExists = await _repository.VerifyDeliveryDriverExistsAsync(
            inbound.DeliveryDriverId,
            DriverLicenseCategory.A,
            cancellationToken);

        if (!deliveryDriverExists)
        {
            validationResult = new ValidationResult(new List<ValidationFailure>
            {
                new(nameof(QueueRentalAgreementRequestInbound.DeliveryDriverId),
                    $"Delivery driver does not exist or does not have a valid driver's license.")
            });

            _outcomeHandler!.RentalAgreementRequestNotQueued(validationResult.ToDictionary());
            return;
        }

        await _useCase.ExecuteAsync(inbound, cancellationToken);
    }

    public void SetOutcomeHandler(IQueueRentalAgreementRequestOutcomeHandler outcomeHandler)
    {
        _outcomeHandler = outcomeHandler;
        _useCase.SetOutcomeHandler(outcomeHandler);
    }
}
