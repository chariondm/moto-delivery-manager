namespace MotoDeliveryManager.Core.Application.UseCases.Motorcycles.UpdateMotorcycleLicensePlate;

public sealed class UpdateMotorcycleLicensePlateUseCase(IUpdateMotorcycleLicensePlateRepository repository)
    : IUpdateMotorcycleLicensePlateUseCase
{
    private IUpdateMotorcycleLicensePlateOutcomeHandler? _outcomeHandler;
    private readonly IUpdateMotorcycleLicensePlateRepository _repository = repository;

    public async Task ExecuteAsync(UpdateMotorcycleLicensePlateInbound inbound)
    {
        var motorcycle = await _repository.UpdateAsync(inbound.MotorcycleId, inbound.LicensePlate);

        if (motorcycle is null)
        {
            _outcomeHandler!.MotorcycleNotFound();

            return;
        }

        _outcomeHandler!.Success(motorcycle);
    }

    public void SetOutcomeHandler(IUpdateMotorcycleLicensePlateOutcomeHandler outcomeHandler) => _outcomeHandler = outcomeHandler;
}
