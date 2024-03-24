namespace MotoDeliveryManager.Core.Application.UseCases.Rentals.QueueRentalAgreementRequest.Inbounds;

/// <summary>
/// Represents the inbound port for the queue rental agreement request use case.
/// </summary>
/// <param name="RentalAgreementId">The identifier of the rental agreement.</param>
/// <param name="DeliveryDriverId">The identifier of the delivery driver.</param>
/// <param name="RentalPlanId">The identifier of the motorcycle rental plan.</param>
/// <param name="ExpectedReturnDate">The expected return date of the motorcycle.</param>
/// <remarks>It is used to queue a rental agreement request for a motorcycle.</remarks>
public record QueueRentalAgreementRequestInbound(
    Guid RentalAgreementId,
    Guid DeliveryDriverId,
    Guid RentalPlanId,
    DateOnly ExpectedReturnDate);
