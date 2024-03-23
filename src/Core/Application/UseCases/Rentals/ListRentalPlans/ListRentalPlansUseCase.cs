namespace MotoDeliveryManager.Core.Application.UseCases.Rentals.ListRentalPlans;

public sealed class ListRentalPlansUseCase(IListRentalPlansRepository repository) : IListRentalPlansUseCase
{
    private IListRentalPlansOutcomeHandler? _outcomeHandler;
    private readonly IListRentalPlansRepository _repository = repository;

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var rentalPlans = await _repository.ListRentalPlansAsync(cancellationToken);

        if (!rentalPlans.Any())
        {
            _outcomeHandler!.NotFoundRentalPlans();

            return;
        }

        _outcomeHandler!.FoundRentalPlans(rentalPlans);
    }

    public void SetOutcomeHandler(IListRentalPlansOutcomeHandler outcomeHandler) => _outcomeHandler = outcomeHandler;
}
