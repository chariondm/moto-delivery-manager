namespace MotoDeliveryManager.UnitTests.Core.Application.UseCases.RegisterMotorcycle;

public class MotorcycleRegistrationValidationTests : TestBase
{
    private readonly Mock<IRegisterMotorcycleRepository> _repository;
    private readonly Mock<IValidator<MotorcycleRegistrationInbound>> _validator;
    private readonly Mock<IMotorcycleRegistrationOutcomeHandler> _outcomeHandler;
    private readonly Mock<IMotorcycleRegistrationUseCase> _useCase;
    private readonly MotorcycleRegistrationValidation _sut;

    public MotorcycleRegistrationValidationTests() : base()
    {
        _repository = Fixture.Freeze<Mock<IRegisterMotorcycleRepository>>();
        _validator = Fixture.Freeze<Mock<IValidator<MotorcycleRegistrationInbound>>>();
        _outcomeHandler = Fixture.Freeze<Mock<IMotorcycleRegistrationOutcomeHandler>>();
        _useCase = Fixture.Freeze<Mock<IMotorcycleRegistrationUseCase>>();

        InitializeTestServices();

        _sut = new MotorcycleRegistrationValidation(ServiceProvider!);
        _sut.SetOutcomeHandler(_outcomeHandler.Object);
    }

    protected override void RegisterTestDependencies(ServiceCollection services)
    {
        services.AddSingleton(_repository.Object);
        services.AddSingleton(_validator.Object);
        services.AddSingleton(_outcomeHandler.Object);

        services
            .AddKeyedSingleton<IMotorcycleRegistrationUseCase, IMotorcycleRegistrationUseCase>(UseCaseType.UseCase, (_, _) => _useCase.Object);

        services.AddSingleton(provider => provider);
    }

    [Fact(DisplayName = "Must Indicate Invalid Entry When Validation Fails")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "RegisterMotorcycle")]
    [Trait("Description", "Ensure motorcycle is not registered when validation fails")]
    public async Task MustIndicateInvalidEntry_WhenValidationFails()
    {
        // Arrange
        var inbound = Fixture.Create<MotorcycleRegistrationInbound>();

        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new("Property", "Error Message")
        });

        _validator.Setup(it => it.ValidateAsync(inbound, CancellationToken.None)).ReturnsAsync(validationResult);

        _outcomeHandler.Setup(x => x.Invalid(It.IsAny<IDictionary<string, string[]>>())).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound);

        // Assert
        _outcomeHandler.Verify(x => x.Invalid(It.IsAny<IDictionary<string, string[]>>()), Times.Once);
    }

    [Fact(DisplayName = "Indicates Duplicated Entry When License Plate Already Exists")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "RegisterMotorcycle")]
    [Trait("Description", "Ensures that the Duplicated method is called on the outcome handler when an attempt is made to register a motorcycle with a license plate that already exists in the repository, preventing duplicate registrations.")]
    public async Task MustIndicateDuplicated_WhenLicensePlateAlreadyExists()
    {
        // Arrange
        var inbound = Fixture.Create<MotorcycleRegistrationInbound>();

        _validator.Setup(v => v.ValidateAsync(inbound, It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult());

        _repository.Setup(x => x.ExistsByLicensePlateAsync(inbound.LicensePlate, default)).ReturnsAsync(true);

        _outcomeHandler.Setup(x => x.Duplicated(It.IsAny<string>())).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound);

        // Assert
        _outcomeHandler.Verify(x => x.Duplicated(It.IsAny<string>()), Times.Once);
    }

    [Fact(DisplayName = "Calls UseCase ExecuteAsync on Successful Validation and Non-Existent License Plate")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "RegisterMotorcycle")]
    [Trait("Description", "Verifies that the ExecuteAsync method of the use case is called when the inbound motorcycle registration passes validation checks and the license plate does not exist in the repository, indicating a new motorcycle registration can proceed.")]
    public async Task MustCallUseCaseExecuteAsync_WhenValidationSucceedsAndLicensePlateDoesNotExist()
    {
        // Arrange
        var inbound = Fixture.Create<MotorcycleRegistrationInbound>();
    
        _validator.Setup(v => v.ValidateAsync(inbound, It.IsAny<CancellationToken>())).ReturnsAsync(new ValidationResult());

        _repository.Setup(x => x.ExistsByLicensePlateAsync(inbound.LicensePlate, default)).ReturnsAsync(false);

        _useCase.Setup(p => p.ExecuteAsync(inbound, default)).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound);

        // Assert
        _useCase.Verify(p => p.ExecuteAsync(inbound, default), Times.Once);
    }
}
