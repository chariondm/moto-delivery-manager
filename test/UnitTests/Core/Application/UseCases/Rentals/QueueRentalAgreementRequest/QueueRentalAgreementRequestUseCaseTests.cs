namespace MotoDeliveryManager.UnitTests.Core.Application.UseCases.Rentals.QueueRentalAgreementRequest;

public class QueueRentalAgreementRequestUseCaseTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IQueueRentalAgreementRequestMessageBroker> _messageBroker;
    private readonly Mock<IQueueRentalAgreementRequestOutcomeHandler> _outcomeHandler;
    private readonly QueueRentalAgreementRequestUseCase _sut;

    public QueueRentalAgreementRequestUseCaseTests()
    {
        _fixture = CustomFixture.CreateFixture();

        _messageBroker = _fixture.Freeze<Mock<IQueueRentalAgreementRequestMessageBroker>>();
        _outcomeHandler = _fixture.Freeze<Mock<IQueueRentalAgreementRequestOutcomeHandler>>();

        _sut = new QueueRentalAgreementRequestUseCase(_messageBroker.Object);
        _sut.SetOutcomeHandler(_outcomeHandler.Object);
    }

    [Fact(DisplayName = "Outcome Handler RentalAgreementRequestQueued Must Be Invoked When Motorcycle Rental Agreement Request Is Queued")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "QueueRentalAgreementRequest")]
    [Trait("Description", "Ensures that 'RentalAgreementRequestQueued' is called on the outcome handler exactly once when motorcycle rental agreement request is queued.")]
    public async Task RentalAgreementRequestQueued_MustBeInvoked_When_MotorcycleRentalAgreementRequestIsQueued()
    {
        // Arrange
        var inbound = _fixture.Create<QueueRentalAgreementRequestInbound>();

        _messageBroker.Setup(x => x.QueueRentalAgreementRequestAsync(inbound, default)).Verifiable();

        _outcomeHandler.Setup(x => x.RentalAgreementRequestQueued(inbound.RentalAgreementId)).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound, default);

        // Assert
        _outcomeHandler.Verify(handler => handler.RentalAgreementRequestQueued(inbound.RentalAgreementId), Times.Once);
    }
}
