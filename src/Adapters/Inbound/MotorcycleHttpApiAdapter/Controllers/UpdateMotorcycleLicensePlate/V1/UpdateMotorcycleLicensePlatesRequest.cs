namespace MotoDeliveryManager.Adapters.Inbound.MotorcycleHttpApiAdapter.Controllers.UpdateMotorcycleLicensePlates.V1;

/// <summary>
/// Request DTO for motorcycle license plate update.
/// </summary>
/// <param name="LicensePlate">License plate of the motorcycle, must follow the Brazilian patterns: ABC1234 or ABC1D23.</param>
public record UpdateMotorcycleLicensePlatesRequest(
    [Required, RegularExpression(@"^[A-Z]{3}\d{4}$|^[A-Z]{3}\d{1}[A-Z]{1}\d{2}$", ErrorMessage = "License plate must follow the Brazilian patterns: ABC1234 or ABC1D23.")] string LicensePlate
);
