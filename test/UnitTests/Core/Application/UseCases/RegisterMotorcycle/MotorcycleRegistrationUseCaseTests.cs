using AutoFixture;
using AutoFixture.AutoMoq;

using Core.Application.UseCases.RegisterMotorcycle;
using Core.Application.UseCases.RegisterMotorcycle.Inbounds;
using Core.Application.UseCases.RegisterMotorcycle.Outbounds;
using Core.Domain.Motorcycles;

using Moq;

namespace UnitTests.Core.Application.UseCases.RegisterMotorcycle;

public class MotorcycleRegistrationUseCaseTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IRegisterMotorcycleRepository> _repository;
    private readonly Mock<IMotorcycleRegistrationOutcomeHandler> _outcomeHandler;
    private readonly MotorcycleRegistrationUseCase _sut;

    public MotorcycleRegistrationUseCaseTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());

        _repository = _fixture.Freeze<Mock<IRegisterMotorcycleRepository>>();
        _outcomeHandler = _fixture.Freeze<Mock<IMotorcycleRegistrationOutcomeHandler>>();

        _sut = _fixture.Create<MotorcycleRegistrationUseCase>();
        _sut.SetOutcomeHandler(_fixture.Freeze<Mock<IMotorcycleRegistrationOutcomeHandler>>().Object);
    }

    [Fact(DisplayName = "Motorcycle Must Be Registered When ExecuteAsync is Called")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "RegisterMotorcycle")]
    [Trait("Description", "Ensure motorcycle is registered with the repository")]
    public async Task Must_RegisterMotorcycle_When_ExecuteAsyncIsCalled()
    {
        // Arrange
        var inbound = _fixture.Create<MotorcycleRegistrationInbound>();

        // Act
        await _sut.ExecuteAsync(inbound);

        // Assert
        _repository.Verify(repo => repo.RegisterAsync(It.IsAny<Motorcycle>(), default), Times.Once);
    }

    [Fact(DisplayName = "OutcomeHandler Must Be Notified When Motorcycle Is Successfully Registered")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "RegisterMotorcycle")]
    [Trait("Description", "Ensure outcome handler is notified after successful registration")]
    public async Task Must_NotifyOutcomeHandler_When_MotorcycleIsSuccessfullyRegistered()
    {
        // Arrange
        var inbound = _fixture.Create<MotorcycleRegistrationInbound>();

        // Act
        await _sut.ExecuteAsync(inbound);

        // Assert
        _outcomeHandler.Verify(handler => handler.Registered(It.IsAny<Guid>()), Times.Once);
    }
}
