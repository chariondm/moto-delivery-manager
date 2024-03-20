namespace Adapters.Inbound.DeliveryDriverHttpApiAdapter.Controllers.RegisterDeliveryDriver.V1;

/// <summary>
/// Represents the response to a successful delivery driver registration.
/// </summary>
/// <param name="Id"></param>
/// <param name="PresignedUrl"></param>
/// <remarks>
/// This record represents the response to a successful delivery driver registration. It is used to pass the necessary data to the
/// delivery driver registration endpoint such as the presigned URL to upload the delivery driver's photo.
/// </remarks>
public record DeliveryDriverRegistrationResponse(Guid Id, string PresignedUrl);
