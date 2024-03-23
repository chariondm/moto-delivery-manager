using AutoFixture;
using AutoFixture.AutoMoq;

using Core.Application.UseCases.UpdateMotorcycleLicensePlate;
using Core.Application.UseCases.UpdateMotorcycleLicensePlate.Inbounds;
using Core.Application.UseCases.UpdateMotorcycleLicensePlate.Outbounds;
using MotoDeliveryManager.Core.Domain.Motorcycles;

using Moq;

namespace UnitTests.Core.Application.UseCases.UpdateMotorcycleLicensePlate;

public class UpdateMotorcycleLicensePlateUseCaseTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IUpdateMotorcycleLicensePlateRepository> _repository;
    private readonly Mock<IUpdateMotorcycleLicensePlateOutcomeHandler> _outcomeHandler;
    private readonly UpdateMotorcycleLicensePlateUseCase _sut;

    public UpdateMotorcycleLicensePlateUseCaseTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());

        _repository = _fixture.Freeze<Mock<IUpdateMotorcycleLicensePlateRepository>>();
        _outcomeHandler = _fixture.Freeze<Mock<IUpdateMotorcycleLicensePlateOutcomeHandler>>();

        _sut = new UpdateMotorcycleLicensePlateUseCase(_repository.Object);
        _sut.SetOutcomeHandler(_outcomeHandler.Object);
    }

    [Fact(DisplayName = "Outcome Handler Success Must Be Invoked When Motorcycle License Plate Is Updated")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "UpdateMotorcycleLicensePlate")]
    [Trait("Description", "Ensures that 'Success' is called on the outcome handler exactly once when the license plate is updated.")]
    public async Task Success_MustBeInvoked_When_MotorcyclesLicensePlateIsUpdated()
    {
        // Arrange
        var inbound = _fixture.Create<UpdateMotorcycleLicensePlateInbound>();
        var motorcycle = _fixture.Create<Motorcycle>();

        _repository.Setup(x => x.UpdateAsync(inbound.MotorcycleId, inbound.LicensePlate, default)).ReturnsAsync(motorcycle);

        _outcomeHandler.Setup(x => x.Success(motorcycle)).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound);

        // Assert
        _outcomeHandler.Verify(handler => handler.Success(motorcycle), Times.Once);
    }

    [Fact(DisplayName = "Outcome Handler MotorcycleNotFound Must Be Invoked When No Motorcycle Is Found")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "UpdateMotorcycleLicensePlate")]
    [Trait("Description", "Ensures that 'MotorcycleNotFound' is called on the outcome handler exactly once when no motorcycle is updated.")]
    public async Task MotorcycleNotFound_MustBeInvoked_When_NoMotorcycleIsFound()
    {
        // Arrange
        var inbound = _fixture.Create<UpdateMotorcycleLicensePlateInbound>();
        var motorcycle = default(Motorcycle);

        _repository.Setup(x => x.UpdateAsync(inbound.MotorcycleId, inbound.LicensePlate, default)).ReturnsAsync(motorcycle);

        _outcomeHandler.Setup(x => x.MotorcycleNotFound()).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound);

        // Assert
        _outcomeHandler.Verify(handler => handler.MotorcycleNotFound(), Times.Once);
    }
}
