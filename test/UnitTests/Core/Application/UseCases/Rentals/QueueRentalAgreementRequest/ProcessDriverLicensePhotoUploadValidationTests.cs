namespace MotoDeliveryManager.UnitTests.Core.Application.UseCases.Rentals.QueueRentalAgreementRequest;

public class QueueRentalAgreementRequestValidationTests : TestBase
{
    private readonly IFixture _fixture;
    private readonly Mock<IQueueRentalAgreementRequestOutcomeHandler> _outcomeHandler;
    private readonly Mock<IValidator<QueueRentalAgreementRequestInbound>> _validator;
    private readonly Mock<IQueueRentalAgreementRequestDeliveryDriverRepository> _repository;
    private readonly Mock<IQueueRentalAgreementRequestUseCase> _useCase;
    private readonly QueueRentalAgreementRequestValidation _sut;

    public QueueRentalAgreementRequestValidationTests() : base()
    {
        _fixture = CustomFixture.CreateFixture();
        _outcomeHandler = Fixture.Freeze<Mock<IQueueRentalAgreementRequestOutcomeHandler>>();
        _validator = Fixture.Freeze<Mock<IValidator<QueueRentalAgreementRequestInbound>>>();
        _repository = Fixture.Freeze<Mock<IQueueRentalAgreementRequestDeliveryDriverRepository>>();
        _useCase = Fixture.Freeze<Mock<IQueueRentalAgreementRequestUseCase>>();

        InitializeTestServices();

        _sut = new QueueRentalAgreementRequestValidation(ServiceProvider!);
        _sut.SetOutcomeHandler(_outcomeHandler.Object);
    }

    protected override void RegisterTestDependencies(ServiceCollection services)
    {
        services.AddSingleton(_outcomeHandler.Object);
        services.AddSingleton(_validator.Object);
        services.AddSingleton(_repository.Object);
        services.AddSingleton(_useCase.Object);

        services
            .AddKeyedSingleton<IQueueRentalAgreementRequestUseCase, IQueueRentalAgreementRequestUseCase>(UseCaseType.UseCase, (_, _) => _useCase.Object);

        services.AddSingleton(provider => provider);
    }

    [Fact(DisplayName = "UseCase ExecuteAsync Must Be Invoked When Inbound Is Valid and Delivery Driver Exists")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "QueueRentalAgreementRequest")]
    [Trait("Description", "Ensures that 'ExecuteAsync' is called on the UseCase exactly once when the inbound is valid and the delivery driver exists.")]
    public async Task ExecuteAsync_MustBeInvoked_When_InboundIsValidAndDeliveryDriverExists()
    {
        // Arrange
        var inbound = _fixture.Create<QueueRentalAgreementRequestInbound>();

        _validator.Setup(x => x.ValidateAsync(inbound, default)).ReturnsAsync(new ValidationResult());

        _repository
            .Setup(x => x.VerifyDeliveryDriverExistsAsync(inbound.DeliveryDriverId, DriverLicenseCategory.A, default))
            .ReturnsAsync(true);

        _useCase.Setup(x => x.ExecuteAsync(inbound, default)).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound, default);

        // Assert
        _useCase.Verify(x => x.ExecuteAsync(inbound, default), Times.Once);
    }

    [Fact(DisplayName = "Outcome Handler RentalAgreementRequestNotValid Must Be Invoked When QueueRentalAgreementRequestInbound Is Not Valid")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "QueueRentalAgreementRequest")]
    [Trait("Description", "Ensures that 'Invalid' is called on the outcome handler exactly once when the QueueRentalAgreementRequestInbound is not valid.")]
    public async Task RentalAgreementRequestNotValid_MustBeInvoked_When_QueueRentalAgreementRequestInboundIsNotValid()
    {
        // Arrange
        var inbound = _fixture.Create<QueueRentalAgreementRequestInbound>();
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new("Property", "Error Message")
        });

        _validator.Setup(x => x.ValidateAsync(inbound, default)).ReturnsAsync(validationResult);

        _outcomeHandler.Setup(x => x.RentalAgreementRequestNotValid(It.IsAny<IDictionary<string, string[]>>())).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound, default);

        // Assert
        _outcomeHandler
            .Verify(handler => handler.RentalAgreementRequestNotValid(It.IsAny<IDictionary<string, string[]>>()), Times.Once);
    }

    [Fact(DisplayName = "Outcome Handler RentalAgreementRequestNotQueued Must Be Invoked When Delivery Driver Does Not Exist")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "QueueRentalAgreementRequest")]
    [Trait("Description", "Ensures that 'RentalAgreementRequestNotQueued' is called on the outcome handler exactly once when the delivery driver does not exist.")]
    public async Task RentalAgreementRequestNotQueued_MustBeInvoked_When_DeliveryDriverDoesNotExist()
    {
        // Arrange
        var inbound = _fixture.Create<QueueRentalAgreementRequestInbound>();

        _validator.Setup(x => x.ValidateAsync(inbound, default)).ReturnsAsync(new ValidationResult());

        _repository
            .Setup(x => x.VerifyDeliveryDriverExistsAsync(inbound.DeliveryDriverId, DriverLicenseCategory.A, default))
            .ReturnsAsync(false);

        _outcomeHandler.Setup(x => x.RentalAgreementRequestNotQueued(It.IsAny<IDictionary<string, string[]>>())).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound, default);

        // Assert
        _outcomeHandler
            .Verify(handler => handler.RentalAgreementRequestNotQueued(It.IsAny<IDictionary<string, string[]>>()), Times.Once);
    }    
}
