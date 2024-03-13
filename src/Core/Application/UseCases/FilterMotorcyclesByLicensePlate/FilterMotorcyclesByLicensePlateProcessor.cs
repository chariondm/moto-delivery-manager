using Core.Application.UseCases.FilterMotorcyclesByLicensePlate.Inbounds;
using Core.Application.UseCases.FilterMotorcyclesByLicensePlate.Outbounds;

namespace Core.Application.UseCases.FilterMotorcyclesByLicensePlate;

public sealed class FilterMotorcyclesByLicensePlateProcessor(IMotorcycleRepository repository) : IFilterMotorcyclesByLicensePlateProcessor
{
    private IFilterMotorcyclesByLicensePlateOutcomeHandler? _outcomeHandler;
    private readonly IMotorcycleRepository _repository = repository;

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
