using FluentValidation;

namespace Core.Application.UseCases.RegisterMotorcycle.Inbounds;

public class MotorcycleRegistrationInboundValidator : AbstractValidator<MotorcycleRegistrationInbound>
{
    public MotorcycleRegistrationInboundValidator()
    {
        RuleFor(x => x.MotorcycleId)
            .NotEmpty();

        RuleFor(x => x.Year)
            .GreaterThan(2000)
            .LessThanOrEqualTo(DateTime.Now.Year);

        RuleFor(x => x.Model)
            .NotEmpty();

        RuleFor(x => x.LicensePlate)
            .NotEmpty()
            .Matches(@"^[A-Z]{3}\d{4}$|^[A-Z]{3}\d{1}[A-Z]{1}\d{2}$")
                .WithMessage("The license plate must follow one of the Brazilian patterns: ABC1234 (old pattern) or ABC1D23 (Mercosul pattern).");
    }
}