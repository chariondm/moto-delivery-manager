using MotoDeliveryManager.Core.Application.UseCases.RegisterMotorcycle.Inbounds;
using MotoDeliveryManager.Core.Application.UseCases.RegisterMotorcycle.Outbounds;
using MotoDeliveryManager.Core.Domain.Motorcycles;

namespace MotoDeliveryManager.Core.Application.UseCases.RegisterMotorcycle;

/// <summary>
/// A concrete processor for handling motorcycle registration.
/// </summary>
public sealed class MotorcycleRegistrationUseCase(IRegisterMotorcycleRepository repository) : IMotorcycleRegistrationUseCase
{
    private IMotorcycleRegistrationOutcomeHandler? _outcomeHandler;
    private readonly IRegisterMotorcycleRepository _repository = repository;

    public async Task ExecuteAsync(MotorcycleRegistrationInbound inbound, CancellationToken cancellationToken = default)
    {
        var motorcycle = new Motorcycle(inbound.MotorcycleId, inbound.Year, inbound.Model, inbound.LicensePlate);

        await _repository.RegisterAsync(motorcycle, cancellationToken);
    
        _outcomeHandler!.Registered(motorcycle.MotorcycleId);
    }

    public void SetOutcomeHandler(IMotorcycleRegistrationOutcomeHandler outcomeHandler) => _outcomeHandler = outcomeHandler;
}
