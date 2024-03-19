using Core.Application.Common;
using Core.Application.UseCases.RegisterDeliveryDriver.Inbounds;
using Core.Application.UseCases.RegisterDeliveryDriver.Outbounds;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace Core.Application.UseCases.RegisterDeliveryDriver;

public sealed class RegisterDeliveryDriverValidation(IServiceProvider serviceProvider) 
    : IRegisterDeliveryDriverUseCase
{
    private IRegisterDeliveryDriverOutcomeHandler? _outcomeHandler;

    private readonly IValidator<RegisterDeliveryDriverInbound> _validator = serviceProvider
        .GetRequiredService<IValidator<RegisterDeliveryDriverInbound>>();

    private readonly IRegisterDeliveryDriverRepository _repository = serviceProvider
        .GetRequiredService<IRegisterDeliveryDriverRepository>();

    private readonly IRegisterDeliveryDriverUseCase _useCase = serviceProvider
        .GetRequiredKeyedService<IRegisterDeliveryDriverUseCase>(UseCaseType.UseCase);

    public async Task ExecuteAsync(RegisterDeliveryDriverInbound inbound, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(inbound);

        if (!validationResult.IsValid)
        {
            _outcomeHandler!.Invalid(validationResult.ToDictionary());
            return;
        }        

        var exists = await _repository.IsCnpjOrDriverLicenseNumberInUseAsync(
            inbound.Cnpj,
            inbound.DriverLicenseNumber,
            cancellationToken);

        if(exists)
        {
            _outcomeHandler!.Duplicated();

            return;
        }

        await _useCase.ExecuteAsync(inbound, cancellationToken);
    }

    public void SetOutcomeHandler(IRegisterDeliveryDriverOutcomeHandler outcomeHandler)
    {
        _outcomeHandler = outcomeHandler;
        _useCase.SetOutcomeHandler(outcomeHandler);
    }
}
