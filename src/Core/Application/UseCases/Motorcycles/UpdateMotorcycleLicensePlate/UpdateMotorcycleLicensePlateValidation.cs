namespace MotoDeliveryManager.Core.Application.UseCases.Motorcycles.UpdateMotorcycleLicensePlate;

public sealed class UpdateMotorcycleLicensePlateValidation(IServiceProvider serviceProvider) 
    : IUpdateMotorcycleLicensePlateUseCase
{
    private IUpdateMotorcycleLicensePlateOutcomeHandler? _outcomeHandler;

    private readonly IValidator<UpdateMotorcycleLicensePlateInbound> _validator = serviceProvider
        .GetRequiredService<IValidator<UpdateMotorcycleLicensePlateInbound>>();

    private readonly IUpdateMotorcycleLicensePlateRepository _repository = serviceProvider
        .GetRequiredService<IUpdateMotorcycleLicensePlateRepository>();

    private readonly IUpdateMotorcycleLicensePlateUseCase _useCase = serviceProvider
        .GetRequiredKeyedService<IUpdateMotorcycleLicensePlateUseCase>(UseCaseType.UseCase);

    public async Task ExecuteAsync(UpdateMotorcycleLicensePlateInbound inbound)
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
            _outcomeHandler!.DuplicateLicensePlate(inbound.LicensePlate);

            return;
        }

        await _useCase.ExecuteAsync(inbound);
    }

    public void SetOutcomeHandler(IUpdateMotorcycleLicensePlateOutcomeHandler outcomeHandler)
    {
        _outcomeHandler = outcomeHandler;
        _useCase.SetOutcomeHandler(outcomeHandler);
    }
}
