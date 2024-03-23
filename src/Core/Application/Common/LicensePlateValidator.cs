namespace MotoDeliveryManager.Core.Application.Common;

public class LicensePlateValidator : AbstractValidator<string?>
{
    public LicensePlateValidator()
    {
        RuleFor(x => x)
            .Matches(@"^[A-Z]{3}\d{4}$|^[A-Z]{3}\d{1}[A-Z]{1}\d{2}$")
            .WithMessage("The license plate must follow one of the Brazilian patterns: ABC1234 (old pattern) or ABC1D23 (Mercosul pattern).");
    }
}
