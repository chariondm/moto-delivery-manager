namespace MotoDeliveryManager.Core.Application.UseCases.DeliveryDrivers.ProcessDriverLicensePhotoUpload;

public static class ProcessDriverLicensePhotoUploadSetup
{
    public static IServiceCollection AddSingletonProcessDriverLicensePhotoUploadUseCase(this IServiceCollection services)
    {
        services
            .AddSingleton<IValidator<ProcessDriverLicensePhotoUploadInbound>, ProcessDriverLicensePhotoUploadInboundValidator>();

        services
            .AddKeyedSingleton<IProcessDriverLicensePhotoUploadUseCase, ProcessDriverLicensePhotoUploadUseCase>(UseCaseType.UseCase)
            .AddKeyedSingleton<IProcessDriverLicensePhotoUploadUseCase, ProcessDriverLicensePhotoUploadValidation>(UseCaseType.Validation);

        return services;
    }
}
