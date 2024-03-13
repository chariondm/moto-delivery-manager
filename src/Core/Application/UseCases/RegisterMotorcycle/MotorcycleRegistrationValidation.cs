using Core.Application.Common;
using Core.Application.UseCases.RegisterMotorcycle.Inbounds;
using Core.Application.UseCases.RegisterMotorcycle.Outbounds;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace Core.Application.UseCases.RegisterMotorcycle;

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

    public async Task ExecuteAsync(MotorcycleRegistrationInbound inbound)
    {
        var validationResult = await _validator.ValidateAsync(inbound);

        if (!validationResult.IsValid)
        {
            _outcomeHandler!.Invalid(validationResult.ToDictionary());
            return;
        }        

        var exists = await _repository.ExistsByLicensePlateAsync(inbound.LicensePlate);

        if(exists)
        {
            _outcomeHandler!.Duplicated(inbound.LicensePlate);

            return;
        }

        await _useCase.ExecuteAsync(inbound);
    }

    public void SetOutcomeHandler(IMotorcycleRegistrationOutcomeHandler outcomeHandler)
    {
        _outcomeHandler = outcomeHandler;
        _useCase.SetOutcomeHandler(outcomeHandler);
    }
}
