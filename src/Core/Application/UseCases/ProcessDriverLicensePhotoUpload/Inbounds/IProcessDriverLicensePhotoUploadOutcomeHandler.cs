namespace MotoDeliveryManager.Core.Application.UseCases.ProcessDriverLicensePhotoUpload.Inbounds;

/// <summary>
/// Handles the outcomes of attempting to process a driver's license photo upload.
/// </summary>
/// <remarks>
/// This interface is used by the ProcessDriverLicensePhotoUpload use case to communicate the outcome of the operation to the
/// inbound adapter.
/// </remarks>
public interface IProcessDriverLicensePhotoUploadOutcomeHandler
{
    /// <summary>
    /// Invoked when the driver's license photo upload is successful.
    /// </summary>
    /// <param name="deliveryDriverId">The unique identifier of the delivery driver.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <remarks>
    /// The delivery driver identifier is provided to the inbound adapter so that it can be used to correlate the
    /// successful outcome with the original request.
    /// </remarks>
    Task SuccessAsync(Guid deliveryDriverId, CancellationToken cancellationToken);

    /// <summary>
    /// Invoked when the input data for the driver's license photo upload is invalid.
    /// </summary>
    /// <param name="errors">A collection of validation errors.</param>
    /// <remarks>
    /// The validation errors are provided to the inbound adapter so that it can be used to communicate the validation
    /// errors to the client.
    /// </remarks>
    void Invalid(IDictionary<string, string[]> errors);

    /// <summary>
    /// Invoked when the delivery driver could not be found.
    /// </summary>
    /// <param name="deliveryDriverId">The unique identifier of the delivery driver.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <remarks>
    /// The delivery driver identifier is provided to the inbound adapter so that it can be used to correlate the
    /// unsuccessful outcome with the original request.
    /// </remarks>  
    Task DeliveryDriverNotFoundAsync(Guid deliveryDriverId, CancellationToken cancellationToken);
}
