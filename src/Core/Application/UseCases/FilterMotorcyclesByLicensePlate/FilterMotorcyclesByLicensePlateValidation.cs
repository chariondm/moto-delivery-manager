using Core.Application.Common;
using Core.Application.UseCases.FilterMotorcyclesByLicensePlate.Inbounds;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace Core.Application.UseCases.FilterMotorcyclesByLicensePlate;

public sealed class FilterMotorcyclesByLicensePlateValidation(IServiceProvider serviceProvider) 
    : IFilterMotorcyclesByLicensePlateProcessor
{
    private IFilterMotorcyclesByLicensePlateOutcomeHandler? _outcomeHandler;

    private readonly IFilterMotorcyclesByLicensePlateProcessor _processor = serviceProvider
        .GetRequiredKeyedService<FilterMotorcyclesByLicensePlateProcessor>(UseCaseType.UseCase);

    public async Task ExecuteAsync(string? licensePlate)
    {
        var validator = new InlineValidator<string?>();
        
        validator.RuleFor(x => x)
            .SetValidator(new LicensePlateValidator())
            .When(x => !string.IsNullOrWhiteSpace(x));

        var validationResult = await validator.ValidateAsync(licensePlate);

        if (!validationResult.IsValid)
        {
            _outcomeHandler!.Invalid(validationResult.ToDictionary());

            return;
        }

        await _processor.ExecuteAsync(licensePlate);
    }

    public void SetOutcomeHandler(IFilterMotorcyclesByLicensePlateOutcomeHandler outcomeHandler)
    {
        _outcomeHandler = outcomeHandler;
        _processor.SetOutcomeHandler(outcomeHandler);
    }
}
