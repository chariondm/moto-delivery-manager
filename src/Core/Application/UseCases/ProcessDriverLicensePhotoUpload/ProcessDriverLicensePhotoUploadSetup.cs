using MotoDeliveryManager.Core.Application.Common;
using MotoDeliveryManager.Core.Application.UseCases.ProcessDriverLicensePhotoUpload.Inbounds;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace MotoDeliveryManager.Core.Application.UseCases.ProcessDriverLicensePhotoUpload;

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
