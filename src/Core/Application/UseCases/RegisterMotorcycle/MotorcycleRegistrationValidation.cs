using Core.Application.Common;
using Core.Application.UseCases.RegisterMotorcycle.Inbounds;
using Core.Application.UseCases.RegisterMotorcycle.Outbounds;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace Core.Application.UseCases.RegisterMotorcycle;

public sealed class MotorcycleRegistrationValidation(IServiceProvider serviceProvider) 
    : IMotorcycleRegistrationProcessor
{
    private IMotorcycleRegistrationOutcomeHandler? _outcomeHandler;

    private readonly IValidator<MotorcycleRegistrationInbound> _validator = serviceProvider
        .GetRequiredService<IValidator<MotorcycleRegistrationInbound>>();

    private readonly IMotorcycleRepository _repository = serviceProvider
        .GetRequiredService<IMotorcycleRepository>();

    private readonly IMotorcycleRegistrationProcessor _processor = serviceProvider
        .GetRequiredKeyedService<IMotorcycleRegistrationProcessor>(UseCaseType.UseCase);

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
            _outcomeHandler!.Duplicated();

            return;
        }

        await _processor.ExecuteAsync(inbound);
    }

    public void SetOutcomeHandler(IMotorcycleRegistrationOutcomeHandler outcomeHandler)
    {
        _outcomeHandler = outcomeHandler;
        _processor.SetOutcomeHandler(outcomeHandler);
    }
}
