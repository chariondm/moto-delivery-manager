using FluentValidation;

namespace MotoDeliveryManager.Core.Application.UseCases.ProcessDriverLicensePhotoUpload.Inbounds;

public class ProcessDriverLicensePhotoUploadInboundValidator : AbstractValidator<ProcessDriverLicensePhotoUploadInbound>
{
    public ProcessDriverLicensePhotoUploadInboundValidator()
    {
        RuleFor(x => x.DeliveryDriverId)
            .NotEmpty();

        RuleFor(x => x.PhotoPath)
            .NotEmpty();
    }
}
