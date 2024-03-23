namespace MotoDeliveryManager.Core.Application.UseCases.Motorcycles.RegisterMotorcycle.Inbounds;

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
            .SetValidator(new LicensePlateValidator());
    }
}
