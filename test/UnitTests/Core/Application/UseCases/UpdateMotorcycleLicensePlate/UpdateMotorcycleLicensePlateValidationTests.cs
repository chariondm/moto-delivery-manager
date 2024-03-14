using AutoFixture;
using AutoFixture.AutoMoq;

using Core.Application.Common;
using Core.Application.UseCases.UpdateMotorcycleLicensePlate;
using Core.Application.UseCases.UpdateMotorcycleLicensePlate.Inbounds;
using Core.Application.UseCases.UpdateMotorcycleLicensePlate.Outbounds;

using FluentValidation;
using FluentValidation.Results;

using Microsoft.Extensions.DependencyInjection;

using Moq;

using UnitTests.Helpers;

namespace UnitTests.Core.Application.UseCases.UpdateMotorcycleLicensePlate;

public class UpdateMotorcycleLicensePlateValidationTests : TestBase
{
    private readonly IFixture _fixture;
    private readonly Mock<IUpdateMotorcycleLicensePlateOutcomeHandler> _outcomeHandler;
    private readonly Mock<IValidator<UpdateMotorcycleLicensePlateInbound>> _validator;
    private readonly Mock<IUpdateMotorcycleLicensePlateRepository> _repository;
    private readonly Mock<IUpdateMotorcycleLicensePlateUseCase> _useCase;
    private readonly UpdateMotorcycleLicensePlateValidation _sut;

    public UpdateMotorcycleLicensePlateValidationTests() : base()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _outcomeHandler = Fixture.Freeze<Mock<IUpdateMotorcycleLicensePlateOutcomeHandler>>();
        _validator = Fixture.Freeze<Mock<IValidator<UpdateMotorcycleLicensePlateInbound>>>();
        _repository = Fixture.Freeze<Mock<IUpdateMotorcycleLicensePlateRepository>>();
        _useCase = Fixture.Freeze<Mock<IUpdateMotorcycleLicensePlateUseCase>>();

        InitializeTestServices();

        _sut = new UpdateMotorcycleLicensePlateValidation(ServiceProvider!);
        _sut.SetOutcomeHandler(_outcomeHandler.Object);
    }

    protected override void RegisterTestDependencies(ServiceCollection services)
    {
        services.AddSingleton(_outcomeHandler.Object);
        services.AddSingleton(_validator.Object);
        services.AddSingleton(_repository.Object);
        services.AddSingleton(_useCase.Object);

        services
            .AddKeyedSingleton<IUpdateMotorcycleLicensePlateUseCase, IUpdateMotorcycleLicensePlateUseCase>(UseCaseType.UseCase, (_, _) => _useCase.Object);

        services.AddSingleton(provider => provider);
    }

    [Fact(DisplayName = "UseCase ExecuteAsync Must Be Invoked When Inbound is valid and no License Plate exists in the database")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "UpdateMotorcycleLicensePlate")]
    [Trait("Description", "Ensures that 'ExecuteAsync' is called on the UseCase exactly once when Inbound is valid and no License Plate exists in the database.")]
    public async Task ExecuteAsync_MustBeInvoked_When_InboundIsValidAndNoLicensePlateExists()
    {
        // Arrange
        var inbound = _fixture.Create<UpdateMotorcycleLicensePlateInbound>();

        _validator.Setup(x => x.ValidateAsync(inbound, CancellationToken.None)).ReturnsAsync(new ValidationResult());

        _repository.Setup(x => x.ExistsByLicensePlateAsync(inbound.LicensePlate)).ReturnsAsync(false);

        _useCase.Setup(x => x.ExecuteAsync(inbound)).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound);

        // Assert
        _useCase.Verify(x => x.ExecuteAsync(inbound), Times.Once);
    }

    [Fact(DisplayName = "Outcome Handler Invalid Must Be Invoked When Inbound Is not valid")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "UpdateMotorcycleLicensePlate")]
    [Trait("Description", "Ensures that 'Invalid' is called on the outcome handler exactly once when the Inbound is not valid.")]
    public async Task Invalid_MustBeInvoked_When_InboundIsNotValid()
    {
        // Arrange
        var inbound = _fixture.Create<UpdateMotorcycleLicensePlateInbound>();
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new("Property", "Error Message")
        });

        _validator.Setup(x => x.ValidateAsync(inbound, CancellationToken.None)).ReturnsAsync(validationResult);

        _outcomeHandler.Setup(x => x.Invalid(It.IsAny<IDictionary<string, string[]>>())).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound);

        // Assert
        _outcomeHandler.Verify(handler => handler.Invalid(It.IsAny<IDictionary<string, string[]>>()), Times.Once);
    }

    [Fact(DisplayName = "Outcome Handler DuplicateLicensePlate Must Be Invoked When License Plate exists in the database")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "UpdateMotorcycleLicensePlate")]
    [Trait("Description", "Ensures that 'DuplicateLicensePlate' is called on the outcome handler exactly once when the License Plate exists.")]
    public async Task DuplicateLicensePlate_MustBeInvoked_When_LicensePlateExists()
    {
        // Arrange
        var inbound = _fixture.Create<UpdateMotorcycleLicensePlateInbound>();

        _validator.Setup(x => x.ValidateAsync(inbound, CancellationToken.None)).ReturnsAsync(new ValidationResult());

        _repository.Setup(x => x.ExistsByLicensePlateAsync(inbound.LicensePlate)).ReturnsAsync(true);

        _outcomeHandler.Setup(x => x.DuplicateLicensePlate(inbound.LicensePlate)).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound);

        // Assert
        _outcomeHandler.Verify(handler => handler.DuplicateLicensePlate(inbound.LicensePlate), Times.Once);
    }
}
