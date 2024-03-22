using AutoFixture;
using AutoFixture.AutoMoq;

using Core.Application.UseCases.Rentals.ListRentalPlans;
using Core.Application.UseCases.Rentals.ListRentalPlans.Inbounds;
using Core.Application.UseCases.Rentals.ListRentalPlans.Outbounds;
using Core.Domain.Rentals;

using Moq;

namespace UnitTests.Core.Application.UseCases.Rentals.ListRentalPlans;

public class ListRentalPlansUseCaseTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IListRentalPlansRepository> _repository;
    private readonly Mock<IListRentalPlansOutcomeHandler> _outcomeHandler;
    private readonly ListRentalPlansUseCase _sut;

    public ListRentalPlansUseCaseTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());

        _repository = _fixture.Freeze<Mock<IListRentalPlansRepository>>();
        _outcomeHandler = _fixture.Freeze<Mock<IListRentalPlansOutcomeHandler>>();

        _sut = new ListRentalPlansUseCase(_repository.Object);
        _sut.SetOutcomeHandler(_outcomeHandler.Object);
    }

    [Fact(DisplayName = "Outcome Handler FoundRentalPlans Must Be Invoked When Motorcycle Rental Plans Are Found")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "ListRentalPlans")]
    [Trait("Description", "Ensures that 'FoundRentalPlans' is called on the outcome handler exactly once when motorcycle rental plans are found.")]
    public async Task FoundRentalPlans_MustBeInvoked_When_MotorcycleRentalPlansAreFound()
    {
        // Arrange
        var rentalPlans = _fixture.CreateMany<RentalPlan>();

        _repository.Setup(x => x.ListRentalPlansAsync(default)).ReturnsAsync(rentalPlans);

        _outcomeHandler.Setup(x => x.FoundRentalPlans(rentalPlans)).Verifiable();

        // Act
        await _sut.ExecuteAsync(default);

        // Assert
        _outcomeHandler.Verify(handler => handler.FoundRentalPlans(rentalPlans), Times.Once);
    }

    [Fact(DisplayName = "Outcome Handler NotFoundRentalPlans Must Be Invoked When Motorcycle Rental Plans Are Not Found")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "ListRentalPlans")]
    [Trait("Description", "Ensures that 'NotFoundRentalPlans' is called on the outcome handler exactly once when motorcycle rental plans are not found.")]
    public async Task NotFoundRentalPlans_MustBeInvoked_When_MotorcycleRentalPlansAreNotFound()
    {
        // Arrange
        _repository.Setup(x => x.ListRentalPlansAsync(default)).ReturnsAsync([]);

        _outcomeHandler.Setup(x => x.NotFoundRentalPlans()).Verifiable();

        // Act
        await _sut.ExecuteAsync(default);

        // Assert
        _outcomeHandler.Verify(handler => handler.NotFoundRentalPlans(), Times.Once);
    }
}
