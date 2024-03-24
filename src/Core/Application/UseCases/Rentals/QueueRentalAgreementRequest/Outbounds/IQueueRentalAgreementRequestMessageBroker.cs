namespace MotoDeliveryManager.Core.Application.UseCases.Rentals.QueueRentalAgreementRequest.Outbounds;

/// <summary>
/// Defines the contract for the message broker queueing the rental agreement request.
/// </summary>
/// <remarks>
/// This message broker is responsible for queueing the rental agreement request.
/// </remarks>
/// <seealso cref="RentalAgreementRequest"/>
/// <seealso cref="CancellationToken"/>
public interface IQueueRentalAgreementRequestMessageBroker
{
    /// <summary>
    /// Queues the rental agreement request.
    /// </summary>
    /// <param name="rentalAgreementRequest">The rental agreement request.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The task representing the asynchronous operation.</returns>
    /// <remarks>It is used to queue the rental agreement request.</remarks>
    /// <seealso cref="RentalAgreementRequest"/>
    /// <seealso cref="CancellationToken"/>
    Task QueueRentalAgreementRequestAsync(
        QueueRentalAgreementRequestInbound rentalAgreementRequest,
        CancellationToken cancellationToken);
}
