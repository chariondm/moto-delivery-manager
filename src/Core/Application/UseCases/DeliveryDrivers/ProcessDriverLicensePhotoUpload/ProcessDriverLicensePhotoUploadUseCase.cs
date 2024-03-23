namespace MotoDeliveryManager.Core.Application.UseCases.DeliveryDrivers.ProcessDriverLicensePhotoUpload;

public sealed class ProcessDriverLicensePhotoUploadUseCase(IProcessDriverLicensePhotoUploadRepository repository)
    : IProcessDriverLicensePhotoUploadUseCase
{
    private IProcessDriverLicensePhotoUploadOutcomeHandler? _outcomeHandler;
    private readonly IProcessDriverLicensePhotoUploadRepository _repository = repository;

    public async Task ExecuteAsync(ProcessDriverLicensePhotoUploadInbound inbound, CancellationToken cancellationToken = default)
    {

        var updatedRows = await _repository.UpdateDriverLicensePhotoPathAsync(
            inbound.DeliveryDriverId,
            inbound.PhotoPath,
            cancellationToken);

        if (updatedRows == 0)
        {
            await _outcomeHandler!.DeliveryDriverNotFoundAsync(inbound.DeliveryDriverId, cancellationToken);

            return;
        }

        await _outcomeHandler!.SuccessAsync(inbound.DeliveryDriverId, cancellationToken);
    }

    public void SetOutcomeHandler(IProcessDriverLicensePhotoUploadOutcomeHandler outcomeHandler) => _outcomeHandler = outcomeHandler;
}
