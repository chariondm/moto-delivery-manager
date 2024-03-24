namespace MotoDeliveryManager.Adapters.Inbound.DeliveryDriverHttpApiAdapter.Controllers.Rentals.QueueRentalAgreementRequest.V1;

/// <summary>
/// Represents the request to queue a rental agreement.
/// </summary>
/// <param name="DeliveryDriverId">The identifier of the delivery driver.</param>
/// <param name="RentalPlanId">The identifier of the motorcycle rental plan.</param>
/// <param name="ExpectedReturnDate">The expected return date of the motorcycle.</param>
/// <remarks>It is used to request a rental agreement for a motorcycle.</remarks>
public record QueueRentalAgreementRequestRequest(
    [Required]
    Guid DeliveryDriverId,
    [Required]
    Guid RentalPlanId,
    [Required]
    [DataType(DataType.Date)]
    DateOnly ExpectedReturnDate);
