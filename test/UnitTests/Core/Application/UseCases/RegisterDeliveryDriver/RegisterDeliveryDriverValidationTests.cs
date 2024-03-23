namespace MotoDeliveryManager.UnitTests.Core.Application.UseCases.RegisterDeliveryDriver;

public class RegisterDeliveryDriverValidationTests : TestBase
{
    private readonly IFixture _fixture;
    private readonly Mock<IRegisterDeliveryDriverOutcomeHandler> _outcomeHandler;
    private readonly Mock<IValidator<RegisterDeliveryDriverInbound>> _validator;
    private readonly Mock<IRegisterDeliveryDriverRepository> _repository;
    private readonly Mock<IRegisterDeliveryDriverUseCase> _useCase;
    private readonly RegisterDeliveryDriverValidation _sut;

    public RegisterDeliveryDriverValidationTests() : base()
    {
        _fixture = CustomFixture.CreateFixture();
        _outcomeHandler = Fixture.Freeze<Mock<IRegisterDeliveryDriverOutcomeHandler>>();
        _validator = Fixture.Freeze<Mock<IValidator<RegisterDeliveryDriverInbound>>>();
        _repository = Fixture.Freeze<Mock<IRegisterDeliveryDriverRepository>>();
        _useCase = Fixture.Freeze<Mock<IRegisterDeliveryDriverUseCase>>();

        InitializeTestServices();

        _sut = new RegisterDeliveryDriverValidation(ServiceProvider!);
        _sut.SetOutcomeHandler(_outcomeHandler.Object);
    }

    protected override void RegisterTestDependencies(ServiceCollection services)
    {
        services.AddSingleton(_outcomeHandler.Object);
        services.AddSingleton(_validator.Object);
        services.AddSingleton(_repository.Object);
        services.AddSingleton(_useCase.Object);

        services
            .AddKeyedSingleton<IRegisterDeliveryDriverUseCase, IRegisterDeliveryDriverUseCase>(UseCaseType.UseCase, (_, _) => _useCase.Object);

        services.AddSingleton(provider => provider);
    }

    [Fact(DisplayName = "UseCase ExecuteAsync Must Be Invoked When Inbound is valid and no Delivery Driver exists in the database")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "RegisterDeliveryDriver")]
    [Trait("Description", "Ensures that 'ExecuteAsync' is called on the UseCase exactly once when Inbound is valid and no Delivery Driver exists in the database.")]
    public async Task ExecuteAsync_MustBeInvoked_When_InboundIsValidAndNoDeliveryDriverExists()
    {
        // Arrange
        var inbound = _fixture.Create<RegisterDeliveryDriverInbound>();

        _validator.Setup(x => x.ValidateAsync(inbound, default)).ReturnsAsync(new ValidationResult());

        _repository.Setup(x => x.IsCnpjOrDriverLicenseNumberInUseAsync(inbound.Cnpj, inbound.DriverLicenseNumber, default))
            .ReturnsAsync(false);

        _useCase.Setup(x => x.ExecuteAsync(inbound, default)).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound, default);

        // Assert
        _useCase.Verify(x => x.ExecuteAsync(inbound, default), Times.Once);
    }

    [Fact(DisplayName = "Outcome Handler Invalid Must Be Invoked When Delivery Driver Inbound Is not valid")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "RegisterDeliveryDriver")]
    [Trait("Description", "Ensures that 'Invalid' is called on the outcome handler exactly once when the delivery driver inbound is not valid.")]
    public async Task Invalid_MustBeInvoked_When_DeliveryDriverInboundIsNotValid()
    {
        // Arrange
        var inbound = _fixture.Create<RegisterDeliveryDriverInbound>();
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new("Property", "Error Message")
        });

        _validator.Setup(x => x.ValidateAsync(inbound, default)).ReturnsAsync(validationResult);

        _outcomeHandler.Setup(x => x.Invalid(It.IsAny<IDictionary<string, string[]>>())).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound);

        // Assert
        _outcomeHandler.Verify(handler => handler.Invalid(It.IsAny<IDictionary<string, string[]>>()), Times.Once);
    }

    [Fact(DisplayName = "Outcome Handler Duplicate Must Be Invoked When the Delivery Driver exists in the database")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "RegisterDeliveryDriver")]
    [Trait("Description", "Ensures that 'Duplicate' is called on the outcome handler exactly once when the delivery driver exists.")]
    public async Task Duplicate_MustBeInvoked_When_DeliveryDriverExists()
    {
        // Arrange
        var inbound = _fixture.Create<RegisterDeliveryDriverInbound>();

        _validator.Setup(x => x.ValidateAsync(inbound, default)).ReturnsAsync(new ValidationResult());

        _repository.Setup(x => x.IsCnpjOrDriverLicenseNumberInUseAsync(inbound.Cnpj, inbound.DriverLicenseNumber, default))
            .ReturnsAsync(true);

        _outcomeHandler.Setup(x => x.Duplicated()).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound);

        // Assert
        _outcomeHandler.Verify(handler => handler.Duplicated(), Times.Once);
    }
}
