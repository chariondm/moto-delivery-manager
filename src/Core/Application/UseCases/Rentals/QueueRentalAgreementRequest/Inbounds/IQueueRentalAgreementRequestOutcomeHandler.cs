namespace MotoDeliveryManager.Core.Application.UseCases.Rentals.QueueRentalAgreementRequest.Inbounds;

/// <summary>
/// Represents the handler for the outcome of the QueueRentalAgreementRequest use case.
/// </summary>
/// <remarks>It is used to notify the caller of the outcome of the use case.</remarks>
/// <seealso cref="RentalAgreement"/>
public interface IQueueRentalAgreementRequestOutcomeHandler
{
    /// <summary>
    /// Invoked when a rental agreement request has not been queued.
    /// </summary>
    /// <param name="errors">A collection of validation errors.</param>
    /// <seealso cref="RentalAgreement"/>
    void RentalAgreementRequestNotQueued(IDictionary<string, string[]> errors);

    /// <summary>
    /// Invoked when a rental agreement request is not valid.
    /// </summary>
    /// <param name="errors">A collection of validation errors.</param>
    /// <seealso cref="RentalAgreement"/>
    void RentalAgreementRequestNotValid(IDictionary<string, string[]> errors);

    /// <summary>
    /// Invoked when a rental agreement request has been successfully queued.
    /// </summary>
    /// <param name="rentalAgreementId">The identifier of the rental agreement.</param>
    /// <seealso cref="RentalAgreement"/>
    void RentalAgreementRequestQueued(Guid rentalAgreementId);
}
