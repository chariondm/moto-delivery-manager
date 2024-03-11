using System.ComponentModel.DataAnnotations;

namespace Adapters.Inbound.HttpApiAdapter.Controllers.RegisterMotorcycles.V1;

/// <summary>
/// Request DTO for registering a new motorcycle.
/// Includes the motorcycle's year of manufacture, model, and license plate.
/// The license plate must follow one of the Brazilian patterns: ABC1234 (old pattern) or ABC1D23 (Mercosul pattern).
/// For more detailed information on the Mercosul license plate standards, refer to the official guidelines.
/// Reference: [Zul Digital Blog on Mercosul Plates 2022](https://www.zuldigital.com.br/blog/placa-mercosul-2022).
/// </summary>
/// <param name="Year">Year of manufacture of the motorcycle.</param>
/// <param name="Model">Model of the motorcycle.</param>
/// <param name="LicensePlate">License plate of the motorcycle, must follow the Brazilian patterns: ABC1234 or ABC1D23.</param>
public record MotorcycleRegistrationRequest(
    [Required] int Year,
    [Required] string Model,
    [Required, RegularExpression(@"^[A-Z]{3}\d{4}$|^[A-Z]{3}\d{1}[A-Z]{1}\d{2}$", ErrorMessage = "License plate must follow the Brazilian patterns: ABC1234 or ABC1D23.")] string LicensePlate
);
