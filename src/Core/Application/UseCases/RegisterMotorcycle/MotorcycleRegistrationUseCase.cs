using Core.Application.UseCases.RegisterMotorcycle.Inbounds;
using Core.Application.UseCases.RegisterMotorcycle.Outbounds;
using Core.Domain;

namespace Core.Application.UseCases.RegisterMotorcycle;

/// <summary>
/// A concrete processor for handling motorcycle registration.
/// </summary>
public sealed class MotorcycleRegistrationUseCase(IRegisterMotorcycleRepository repository) : IMotorcycleRegistrationUseCase
{
    private IMotorcycleRegistrationOutcomeHandler? _outcomeHandler;
    private readonly IRegisterMotorcycleRepository _repository = repository;

    public async Task ExecuteAsync(MotorcycleRegistrationInbound inbound)
    {
        var motorcycle = new Motorcycle(inbound.MotorcycleId, inbound.Year, inbound.Model, inbound.LicensePlate);

        await _repository.RegisterAsync(motorcycle);
    
        _outcomeHandler!.Registered(motorcycle.MotorcycleId);
    }

    public void SetOutcomeHandler(IMotorcycleRegistrationOutcomeHandler outcomeHandler) => _outcomeHandler = outcomeHandler;
}
