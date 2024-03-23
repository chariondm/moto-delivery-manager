using MotoDeliveryManager.Core.Application.Common;

using FluentValidation;

namespace MotoDeliveryManager.Core.Application.UseCases.UpdateMotorcycleLicensePlate.Inbounds;

public class UpdateMotorcycleLicensePlateInboundValidator : AbstractValidator<UpdateMotorcycleLicensePlateInbound>
{
    public UpdateMotorcycleLicensePlateInboundValidator()
    {
        RuleFor(x => x.MotorcycleId)
            .NotEmpty();

        RuleFor(x => x.LicensePlate)
            .NotEmpty()
            .SetValidator(new LicensePlateValidator());
    }
}
