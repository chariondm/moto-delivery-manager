using AutoFixture;
using AutoFixture.AutoMoq;

using Core.Application.UseCases.FilterMotorcyclesByLicensePlate;
using Core.Application.UseCases.FilterMotorcyclesByLicensePlate.Inbounds;
using Core.Application.UseCases.FilterMotorcyclesByLicensePlate.Outbounds;
using Core.Domain.Motorcycles;

using Moq;

namespace UnitTests.Core.Application.UseCases.FilterMotorcyclesByLicensePlate;

public class UpdateMotorcycleLicensePlateUseCaseTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IFilterMotorcyclesByLicensePlateRepository> _repository;
    private readonly Mock<IFilterMotorcyclesByLicensePlateOutcomeHandler> _outcomeHandler;
    private readonly FilterMotorcyclesByLicensePlateUseCase _sut;

    public UpdateMotorcycleLicensePlateUseCaseTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());

        _repository = _fixture.Freeze<Mock<IFilterMotorcyclesByLicensePlateRepository>>();
        _outcomeHandler = _fixture.Freeze<Mock<IFilterMotorcyclesByLicensePlateOutcomeHandler>>();

        _sut = new FilterMotorcyclesByLicensePlateUseCase(_repository.Object);
        _sut.SetOutcomeHandler(_outcomeHandler.Object);
    }

    [Fact(DisplayName = "OnMotorcyclesFound Must Be Invoked When Motorcycles Are Found")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "FilterMotorcyclesByLicensePlate")]
    [Trait("Description", "Verifies that OnMotorcyclesFound is called on the outcome handler exactly once when motorcycles matching the license plate are found.")]
    public async Task OnMotorcyclesFound_MustBeInvoked_When_MotorcyclesAreFound()
    {
        // Arrange
        var inbound = _fixture.Create<string>();
        var motorcycles = _fixture.CreateMany<Motorcycle>();

        _repository.Setup(x => x.FindByLicensePlateAsync(inbound, default)).ReturnsAsync(motorcycles);

        _outcomeHandler.Setup(x => x.OnMotorcyclesFound(motorcycles)).Verifiable();


        // Act
        await _sut.ExecuteAsync(inbound);

        // Assert
        _outcomeHandler.Verify(handler => handler.OnMotorcyclesFound(motorcycles), Times.Once);
    }

    [Fact(DisplayName = "OnMotorcyclesNotFound Must Be Invoked When No Motorcycles Are Found")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "FilterMotorcyclesByLicensePlate")]
    [Trait("Description", "Ensures that OnMotorcyclesNotFound is called on the outcome handler exactly once when no motorcycles match the license plate.")]
    public async Task OnMotorcyclesNotFound_MustBeInvoked_When_NoMotorcyclesAreFound()
    {
        // Arrange
        var inbound = _fixture.Create<string>();
        var motorcycles = Enumerable.Empty<Motorcycle>();

        _repository.Setup(x => x.FindByLicensePlateAsync(inbound, default)).ReturnsAsync(motorcycles);

        _outcomeHandler.Setup(x => x.OnMotorcyclesNotFound()).Verifiable();


        // Act
        await _sut.ExecuteAsync(inbound);

        // Assert
        _outcomeHandler.Verify(handler => handler.OnMotorcyclesNotFound(), Times.Once);
    }
}
