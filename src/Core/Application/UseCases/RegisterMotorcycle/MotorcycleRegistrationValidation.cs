using MotoDeliveryManager.Core.Application.Common;
using MotoDeliveryManager.Core.Application.UseCases.RegisterMotorcycle.Inbounds;
using MotoDeliveryManager.Core.Application.UseCases.RegisterMotorcycle.Outbounds;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace MotoDeliveryManager.Core.Application.UseCases.RegisterMotorcycle;

public sealed class MotorcycleRegistrationValidation(IServiceProvider serviceProvider) 
    : IMotorcycleRegistrationUseCase
{
    private IMotorcycleRegistrationOutcomeHandler? _outcomeHandler;

    private readonly IValidator<MotorcycleRegistrationInbound> _validator = serviceProvider
        .GetRequiredService<IValidator<MotorcycleRegistrationInbound>>();

    private readonly IRegisterMotorcycleRepository _repository = serviceProvider
        .GetRequiredService<IRegisterMotorcycleRepository>();

    private readonly IMotorcycleRegistrationUseCase _useCase = serviceProvider
        .GetRequiredKeyedService<IMotorcycleRegistrationUseCase>(UseCaseType.UseCase);

    public async Task ExecuteAsync(MotorcycleRegistrationInbound inbound, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(inbound);

        if (!validationResult.IsValid)
        {
            _outcomeHandler!.Invalid(validationResult.ToDictionary());
            return;
        }        

        var exists = await _repository.ExistsByLicensePlateAsync(inbound.LicensePlate, cancellationToken);

        if(exists)
        {
            _outcomeHandler!.Duplicated(inbound.LicensePlate);

            return;
        }

        await _useCase.ExecuteAsync(inbound, cancellationToken);
    }

    public void SetOutcomeHandler(IMotorcycleRegistrationOutcomeHandler outcomeHandler)
    {
        _outcomeHandler = outcomeHandler;
        _useCase.SetOutcomeHandler(outcomeHandler);
    }
}
