namespace MotoDeliveryManager.Core.Application.UseCases.DeliveryDrivers.ProcessDriverLicensePhotoUpload.Inbounds;

/// <summary>
/// Inbound port for the ProcessDriverLicensePhotoUpload use case.
/// </summary>
/// <remarks>
/// This inbound port is used by the ProcessDriverLicensePhotoUpload use case to receive the driver's license photo from
/// the inbound adapter.
/// </remarks>
public interface IProcessDriverLicensePhotoUploadUseCase
{
    /// <summary>
    /// Executes the use case asynchronously.
    /// </summary>
    /// <param name="inbound">The inbound data for the use case.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <remarks>
    /// The inbound data is provided by the inbound adapter and contains the driver's license photo to be processed.
    /// </remarks>
    /// <seealso cref="ProcessDriverLicensePhotoUploadInbound"/>
    Task ExecuteAsync(ProcessDriverLicensePhotoUploadInbound inbound, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the outcome handler for the use case.
    /// </summary>
    /// <param name="outcomeHandler">The outcome handler.</param> 
    /// <remarks>
    /// The outcome handler is used by the use case to communicate the outcome of the operation to the inbound adapter.
    /// </remarks>
    /// <seealso cref="IProcessDriverLicensePhotoUploadOutcomeHandler"/>
    void SetOutcomeHandler(IProcessDriverLicensePhotoUploadOutcomeHandler outcomeHandler);
}
