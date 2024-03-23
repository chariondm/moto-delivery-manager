namespace MotoDeliveryManager.Core.Application.UseCases.Motorcycles.UpdateMotorcycleLicensePlate.Inbounds;

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
