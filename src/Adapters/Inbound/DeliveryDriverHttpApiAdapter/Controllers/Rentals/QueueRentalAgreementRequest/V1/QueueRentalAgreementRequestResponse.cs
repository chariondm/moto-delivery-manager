namespace MotoDeliveryManager.Adapters.Inbound.DeliveryDriverHttpApiAdapter.Controllers.Rentals.QueueRentalAgreementRequest.V1;

/// <summary>
/// Represents the response for a queued rental agreement request.
/// </summary>
/// <param name="RentalAgreementId"></param>
/// <remarks>It is used to describe the outcome of a rental agreement request.</remarks>
public record QueueRentalAgreementRequestResponse(Guid RentalAgreementId);
