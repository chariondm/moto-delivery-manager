namespace MotoDeliveryManager.Core.Application.UseCases.Motorcycles.FilterMotorcyclesByLicensePlate;

public sealed class FilterMotorcyclesByLicensePlateValidation(IServiceProvider serviceProvider) 
    : IFilterMotorcyclesByLicensePlateUseCase
{
    private IFilterMotorcyclesByLicensePlateOutcomeHandler? _outcomeHandler;

    private readonly IFilterMotorcyclesByLicensePlateUseCase _useCase = serviceProvider
        .GetRequiredKeyedService<IFilterMotorcyclesByLicensePlateUseCase>(UseCaseType.UseCase);

    public async Task ExecuteAsync(string? licensePlate)
    {
        var validator = new InlineValidator<string?>();
        
        validator.RuleFor(x => x)
            .SetValidator(new LicensePlateValidator())
            .When(x => !string.IsNullOrWhiteSpace(x));

        var validationResult = await validator.ValidateAsync(licensePlate ?? string.Empty);

        if (!validationResult.IsValid)
        {
            _outcomeHandler!.Invalid(validationResult.ToDictionary());

            return;
        }

        await _useCase.ExecuteAsync(licensePlate);
    }

    public void SetOutcomeHandler(IFilterMotorcyclesByLicensePlateOutcomeHandler outcomeHandler)
    {
        _outcomeHandler = outcomeHandler;
        _useCase.SetOutcomeHandler(outcomeHandler);
    }
}
