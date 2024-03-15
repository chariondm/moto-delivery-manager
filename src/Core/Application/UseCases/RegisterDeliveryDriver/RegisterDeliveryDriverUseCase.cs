using Core.Application.UseCases.RegisterDeliveryDriver.Inbounds;
using Core.Application.UseCases.RegisterDeliveryDriver.Outbounds;

using Core.Domain.DeliveryDrivers;

namespace Core.Application.UseCases.RegisterDeliveryDriver;

public sealed class RegisterDeliveryDriverUseCase(IRegisterDeliveryDriverRepository repository) : IRegisterDeliveryDriverUseCase
{
    private IRegisterDeliveryDriverOutcomeHandler? _outcomeHandler;
    private readonly IRegisterDeliveryDriverRepository _repository = repository;

    public async Task ExecuteAsync(RegisterDeliveryDriverInbound inbound)
    {
        var deliveryDriver = new DeliveryDriver(inbound.DeliveryDriverId, inbound.Name, inbound.Cnpj, inbound.DateOfBirth,
            new DriverLicense(inbound.DriverLicenseNumber, inbound.DriverLicenseType, null));

        await _repository.RegisterDeliveryDriverAsync(deliveryDriver);
    
        _outcomeHandler!.Registered(inbound.DeliveryDriverId);
    }

    public void SetOutcomeHandler(IRegisterDeliveryDriverOutcomeHandler outcomeHandler) => _outcomeHandler = outcomeHandler;
}
