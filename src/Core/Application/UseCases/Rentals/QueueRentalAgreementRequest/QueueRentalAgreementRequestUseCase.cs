namespace MotoDeliveryManager.Core.Application.UseCases.Rentals.QueueRentalAgreementRequest;

public sealed class QueueRentalAgreementRequestUseCase(IQueueRentalAgreementRequestMessageBroker messageBroker)
    : IQueueRentalAgreementRequestUseCase
{
    private IQueueRentalAgreementRequestOutcomeHandler? _outcomeHandler;
    private readonly IQueueRentalAgreementRequestMessageBroker _messageBroker = messageBroker;

    public async Task ExecuteAsync(QueueRentalAgreementRequestInbound inbound, CancellationToken cancellationToken)
    {
        await _messageBroker.QueueRentalAgreementRequestAsync(inbound, cancellationToken);

        _outcomeHandler?.RentalAgreementRequestQueued(inbound.RentalAgreementId);
    }

    public void SetOutcomeHandler(IQueueRentalAgreementRequestOutcomeHandler outcomeHandler) => _outcomeHandler = outcomeHandler;
}
