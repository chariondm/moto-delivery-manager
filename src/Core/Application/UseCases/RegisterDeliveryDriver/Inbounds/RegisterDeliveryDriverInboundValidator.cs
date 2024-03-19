using FluentValidation;

namespace Core.Application.UseCases.RegisterDeliveryDriver.Inbounds;

public class RegisterDeliveryDriverInboundValidator : AbstractValidator<RegisterDeliveryDriverInbound>
{
    public RegisterDeliveryDriverInboundValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Cnpj)
            .NotEmpty()
            .IsValidCNPJ().WithMessage("'{PropertyName}' is not a valid CNPJ.");

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateOnly.FromDateTime(DateTime.UtcNow));

        RuleFor(x => x.DriverLicenseNumber)
            .NotEmpty();

        RuleFor(x => x.DriverLicenseCategory)
            .IsInEnum();
    }
}
