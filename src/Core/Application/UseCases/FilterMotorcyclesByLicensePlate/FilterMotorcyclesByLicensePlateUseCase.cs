using MotoDeliveryManager.Core.Application.UseCases.FilterMotorcyclesByLicensePlate.Inbounds;
using MotoDeliveryManager.Core.Application.UseCases.FilterMotorcyclesByLicensePlate.Outbounds;

namespace MotoDeliveryManager.Core.Application.UseCases.FilterMotorcyclesByLicensePlate;

public sealed class FilterMotorcyclesByLicensePlateUseCase(IFilterMotorcyclesByLicensePlateRepository repository) : IFilterMotorcyclesByLicensePlateUseCase
{
    private IFilterMotorcyclesByLicensePlateOutcomeHandler? _outcomeHandler;
    private readonly IFilterMotorcyclesByLicensePlateRepository _repository = repository;

    public async Task ExecuteAsync(string? licensePlate)
    {
        var motorcycles = await _repository.FindByLicensePlateAsync(licensePlate);

        if (!motorcycles.Any())
        {
            _outcomeHandler!.OnMotorcyclesNotFound();

            return;
        }

        _outcomeHandler!.OnMotorcyclesFound(motorcycles);
    }

    public void SetOutcomeHandler(IFilterMotorcyclesByLicensePlateOutcomeHandler outcomeHandler) => _outcomeHandler = outcomeHandler;
}
