namespace MotoDeliveryManager.Core.Application.UseCases.Rentals.QueueRentalAgreementRequest.Inbounds;

public class QueueRentalAgreementRequestInboundValidator : AbstractValidator<QueueRentalAgreementRequestInbound>
{
    public QueueRentalAgreementRequestInboundValidator()
    {
        RuleFor(x => x.RentalAgreementId)
            .NotEmpty();

        RuleFor(x => x.DeliveryDriverId)
            .NotEmpty();

        RuleFor(x => x.RentalPlanId)
            .NotEmpty();

        RuleFor(x => x.ExpectedReturnDate)
            .NotEmpty()
            .GreaterThan(DateOnly.FromDateTime(DateTime.Today));
    }
}
