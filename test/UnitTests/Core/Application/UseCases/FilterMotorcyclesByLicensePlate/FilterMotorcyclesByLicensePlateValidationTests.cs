using AutoFixture;

using Core.Application.Common;
using Core.Application.UseCases.FilterMotorcyclesByLicensePlate;
using Core.Application.UseCases.FilterMotorcyclesByLicensePlate.Inbounds;

using Microsoft.Extensions.DependencyInjection;

using Moq;

using UnitTests.Helpers;

namespace UnitTests.Core.Application.UseCases.FilterMotorcyclesByLicensePlate;

public class FilterMotorcyclesByLicensePlateValidationTests : TestBase
{
    private readonly Mock<IFilterMotorcyclesByLicensePlateOutcomeHandler> _outcomeHandler;
    private readonly Mock<IFilterMotorcyclesByLicensePlateUseCase> _useCase;
    private readonly FilterMotorcyclesByLicensePlateValidation _sut;

    public FilterMotorcyclesByLicensePlateValidationTests() : base()
    {
        _outcomeHandler = Fixture.Freeze<Mock<IFilterMotorcyclesByLicensePlateOutcomeHandler>>();
        _useCase = Fixture.Freeze<Mock<IFilterMotorcyclesByLicensePlateUseCase>>();

        InitializeTestServices();

        _sut = new FilterMotorcyclesByLicensePlateValidation(ServiceProvider!);
        _sut.SetOutcomeHandler(_outcomeHandler.Object);
    }

    protected override void RegisterTestDependencies(ServiceCollection services)
    {
        services.AddSingleton(_outcomeHandler.Object);

        services
            .AddKeyedSingleton<IFilterMotorcyclesByLicensePlateUseCase, IFilterMotorcyclesByLicensePlateUseCase>(UseCaseType.UseCase, (_, _) => _useCase.Object);

        services.AddSingleton(provider => provider);
    }

    [Theory(DisplayName = "ExecuteAsync Must Invoke UseCase Exactly Once With Given License Plate")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "FilterMotorcyclesByLicensePlate")]
    [Trait("Description", "Ensures that the FilterMotorcyclesByLicensePlateValidation's ExecuteAsync method invokes the underlying use case exactly once with the provided license plate, regardless of its content being null, empty, or valid.")]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("ABC1234")]
    public async Task ExecuteAsync_MustInvokeUseCaseExactlyOnce_WithGivenLicensePlate(string? acceptedLicensePlate)
    {
        // Arrange
        _useCase.Setup(x => x.ExecuteAsync(acceptedLicensePlate)).Verifiable();

        // Act
        await _sut.ExecuteAsync(acceptedLicensePlate);

        // Assert
        _useCase.Verify(x => x.ExecuteAsync(acceptedLicensePlate), Times.Once);
    }

    [Fact(DisplayName = "ExecuteAsync Must Invoke Invalid Outcome Handler Once With Invalid License Plate")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "FilterMotorcyclesByLicensePlate")]
    [Trait("Description", "Verifies that the FilterMotorcyclesByLicensePlateValidation's ExecuteAsync method invokes the Invalid outcome handler exactly once when provided with an invalid license plate.")]
    public async Task ExecuteAsync_MustInvokeInvalidOutcomeHandlerOnce_WithInvalidLicensePlate()
    {
        // Arrange
        var invalidLicensePlate = Fixture.Create<string>();

        _outcomeHandler.Setup(x => x.Invalid(It.IsAny<IDictionary<string, string[]>>())).Verifiable();

        // Act
        await _sut.ExecuteAsync(invalidLicensePlate);

        // Assert
        _outcomeHandler.Verify(x => x.Invalid(It.IsAny<IDictionary<string, string[]>>()), Times.Once);
    }
}
