using AutoFixture;

using Core.Application.Common;
using Core.Application.UseCases.ProcessDriverLicensePhotoUpload;
using Core.Application.UseCases.ProcessDriverLicensePhotoUpload.Inbounds;

using FluentValidation;
using FluentValidation.Results;

using Microsoft.Extensions.DependencyInjection;

using Moq;

using UnitTests.Helpers;

namespace UnitTests.Core.Application.UseCases.ProcessDriverLicensePhotoUpload;

public class ProcessDriverLicensePhotoUploadValidationTests : TestBase
{
    private readonly IFixture _fixture;
    private readonly Mock<IProcessDriverLicensePhotoUploadOutcomeHandler> _outcomeHandler;
    private readonly Mock<IValidator<ProcessDriverLicensePhotoUploadInbound>> _validator;
    private readonly Mock<IProcessDriverLicensePhotoUploadUseCase> _useCase;
    private readonly ProcessDriverLicensePhotoUploadValidation _sut;

    public ProcessDriverLicensePhotoUploadValidationTests() : base()
    {
        _fixture = CustomFixture.CreateFixture();
        _outcomeHandler = Fixture.Freeze<Mock<IProcessDriverLicensePhotoUploadOutcomeHandler>>();
        _validator = Fixture.Freeze<Mock<IValidator<ProcessDriverLicensePhotoUploadInbound>>>();
        _useCase = Fixture.Freeze<Mock<IProcessDriverLicensePhotoUploadUseCase>>();

        InitializeTestServices();

        _sut = new ProcessDriverLicensePhotoUploadValidation(ServiceProvider!);
        _sut.SetOutcomeHandler(_outcomeHandler.Object);
    }

    protected override void RegisterTestDependencies(ServiceCollection services)
    {
        services.AddSingleton(_outcomeHandler.Object);
        services.AddSingleton(_validator.Object);
        services.AddSingleton(_useCase.Object);

        services
            .AddKeyedSingleton<IProcessDriverLicensePhotoUploadUseCase, IProcessDriverLicensePhotoUploadUseCase>(UseCaseType.UseCase, (_, _) => _useCase.Object);

        services.AddSingleton(provider => provider);
    }

    [Fact(DisplayName = "UseCase ExecuteAsync Must Be Invoked When ProcessDriverLicensePhotoUploadInbound Is Valid")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "ProcessDriverLicensePhotoUpload")]
    [Trait("Description", "Ensures that 'ExecuteAsync' is called on the UseCase exactly once when the ProcessDriverLicensePhotoUploadInbound is valid.")]
    public async Task ExecuteAsync_MustBeInvoked_When_ProcessDriverLicensePhotoUploadInboundIsValid()
    {
        // Arrange
        var inbound = _fixture.Create<ProcessDriverLicensePhotoUploadInbound>();

        _validator.Setup(x => x.ValidateAsync(inbound, default)).ReturnsAsync(new ValidationResult());

        _useCase.Setup(x => x.ExecuteAsync(inbound, default)).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound, default);

        // Assert
        _useCase.Verify(x => x.ExecuteAsync(inbound, default), Times.Once);
    }

    [Fact(DisplayName = "Outcome Handler Invalid Must Be Invoked When ProcessDriverLicensePhotoUploadInbound Is Not Valid")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "ProcessDriverLicensePhotoUpload")]
    [Trait("Description", "Ensures that 'Invalid' is called on the outcome handler exactly once when the ProcessDriverLicensePhotoUploadInbound is not valid.")]
    public async Task Invalid_MustBeInvoked_When_ProcessDriverLicensePhotoUploadInboundIsNotValid()
    {
        // Arrange
        var inbound = _fixture.Create<ProcessDriverLicensePhotoUploadInbound>();
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
}
