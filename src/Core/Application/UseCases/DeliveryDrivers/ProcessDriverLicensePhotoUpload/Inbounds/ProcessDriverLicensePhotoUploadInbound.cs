namespace MotoDeliveryManager.Core.Application.UseCases.DeliveryDrivers.ProcessDriverLicensePhotoUpload.Inbounds;

/// <summary>
/// Inbound port for the ProcessDriverLicensePhotoUpload use case.
/// </summary>
/// <param name="DeliveryDriverId">The delivery driver's unique identifier.</param>
/// <param name="PhotoPath">The path to the driver's license photo.</param>
/// <remarks>
/// This inbound port is used by the ProcessDriverLicensePhotoUpload use case to receive the driver's license photo from
/// the inbound adapter.
/// </remarks>
public record ProcessDriverLicensePhotoUploadInbound(Guid DeliveryDriverId, string PhotoPath);
