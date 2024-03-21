using Core.Application.Common;
using Core.Application.UseCases.ProcessDriverLicensePhotoUpload.Inbounds;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace Core.Application.UseCases.ProcessDriverLicensePhotoUpload;

public sealed class ProcessDriverLicensePhotoUploadValidation(IServiceProvider serviceProvider)
    : IProcessDriverLicensePhotoUploadUseCase
{
    private IProcessDriverLicensePhotoUploadOutcomeHandler? _outcomeHandler;

    private readonly IValidator<ProcessDriverLicensePhotoUploadInbound> _validator = serviceProvider
        .GetRequiredService<IValidator<ProcessDriverLicensePhotoUploadInbound>>();

    private readonly IProcessDriverLicensePhotoUploadUseCase _useCase = serviceProvider
        .GetRequiredKeyedService<IProcessDriverLicensePhotoUploadUseCase>(UseCaseType.UseCase);

    public async Task ExecuteAsync(ProcessDriverLicensePhotoUploadInbound inbound, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(inbound);

        if (!validationResult.IsValid)
        {
            _outcomeHandler!.Invalid(validationResult.ToDictionary());
            return;
        }

        await _useCase.ExecuteAsync(inbound, cancellationToken);
    }

    public void SetOutcomeHandler(IProcessDriverLicensePhotoUploadOutcomeHandler outcomeHandler)
    {
        _outcomeHandler = outcomeHandler;
        _useCase.SetOutcomeHandler(outcomeHandler);
    }
}