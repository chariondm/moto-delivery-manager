using Core.Application.Common;

using FluentValidation;

namespace Core.Application.UseCases.RegisterMotorcycle.Inbounds;

public class UpdateMotorcycleLicensePlateInboundValidator : AbstractValidator<MotorcycleRegistrationInbound>
{
    public UpdateMotorcycleLicensePlateInboundValidator()
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
            .SetValidator(new LicensePlateValidator());
    }
}
