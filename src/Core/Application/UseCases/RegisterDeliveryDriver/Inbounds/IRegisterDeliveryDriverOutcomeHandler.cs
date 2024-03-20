namespace Core.Application.UseCases.RegisterDeliveryDriver.Inbounds;

/// <summary>
/// Defines the contract for handling the outcomes of the Delivery Driver registration use case.
/// </summary>
public interface IRegisterDeliveryDriverOutcomeHandler
{
    /// <summary>
    /// Handles the scenario where a CNPJ or driver's license number is already in use.
    /// </summary>
    /// <remarks>
    /// This method is called when the use case detects that the CNPJ or driver's license number
    /// is already in use by another Delivery Driver.
    /// </remarks>
    void Duplicated();

    /// <summary>
    /// Handles the scenario where the use case detects that the inbound data is invalid.
    /// </summary>
    /// <param name="errors">The validation errors.</param>
    /// <remarks>
    /// This method is called when the use case detects that the inbound data is invalid.
    /// </remarks>
    void Invalid(IDictionary<string, string[]> errors);

    /// <summary>
    /// Handles the scenario where the use case successfully registers a Delivery Driver.
    /// </summary>
    /// <param name="deliveryDriverId">The Delivery Driver's unique identifier.</param>
    /// <param name="presignedUrl">The presigned URL to upload the Delivery Driver's license photo.</param>
    /// <remarks>
    /// This method is called when the use case successfully registers a Delivery Driver.
    /// </remarks>
    void Registered(Guid deliveryDriverId, string presignedUrl);
}
