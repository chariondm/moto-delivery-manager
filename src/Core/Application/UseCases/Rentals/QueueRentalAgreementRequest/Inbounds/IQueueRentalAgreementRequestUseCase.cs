namespace MotoDeliveryManager.Core.Application.UseCases.Rentals.QueueRentalAgreementRequest.Inbounds;

/// <summary>
/// Represents the inbound port for the queue rental agreement request use case.
/// </summary>
/// <remarks>It is used to queue a rental agreement request for a motorcycle.</remarks>
public interface IQueueRentalAgreementRequestUseCase
{
    /// <summary>
    /// Executes the queue rental agreement request use case asynchronously.
    /// </summary>
    /// <param name="inbound">The inbound data for the queue rental agreement request use case.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The task representing the asynchronous operation.</returns>
    /// <remarks>It is used to queue a rental agreement request for a motorcycle.</remarks>
    /// <seealso cref="QueueRentalAgreementRequestInbound"/>
    /// <seealso cref="CancellationToken"/>
    Task ExecuteAsync(QueueRentalAgreementRequestInbound inbound, CancellationToken cancellationToken);

    /// <summary>
    /// Sets the outcome handler for the queue rental agreement request use case.
    /// </summary>
    /// <param name="outcomeHandler">The outcome handler for the queue rental agreement request use case.</param>
    /// <remarks>It is used to set the outcome handler for the queue rental agreement request use case.</remarks>
    /// <seealso cref="IQueueRentalAgreementRequestOutcomeHandler"/>
    void SetOutcomeHandler(IQueueRentalAgreementRequestOutcomeHandler outcomeHandler);
}
