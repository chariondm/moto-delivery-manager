using AutoFixture;

using MotoDeliveryManager.Core.Application.UseCases.ProcessDriverLicensePhotoUpload;
using MotoDeliveryManager.Core.Application.UseCases.ProcessDriverLicensePhotoUpload.Inbounds;
using MotoDeliveryManager.Core.Application.UseCases.ProcessDriverLicensePhotoUpload.Outbounds;

using Moq;

using UnitTests.Helpers;

namespace UnitTests.Core.Application.UseCases.ProcessDriverLicensePhotoUpload;

public class ProcessDriverLicensePhotoUploadUseCaseTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IProcessDriverLicensePhotoUploadRepository> _repository;
    private readonly Mock<IProcessDriverLicensePhotoUploadOutcomeHandler> _outcomeHandler;
    private readonly ProcessDriverLicensePhotoUploadUseCase _sut;

    public ProcessDriverLicensePhotoUploadUseCaseTests()
    {
        _fixture = CustomFixture.CreateFixture();

        _repository = _fixture.Freeze<Mock<IProcessDriverLicensePhotoUploadRepository>>();
        _outcomeHandler = _fixture.Freeze<Mock<IProcessDriverLicensePhotoUploadOutcomeHandler>>();

        _sut = new ProcessDriverLicensePhotoUploadUseCase(_repository.Object);
        _sut.SetOutcomeHandler(_outcomeHandler.Object);
    }

    [Fact(DisplayName = "Outcome Handler SuccessAsync Must Be Invoked When the Driver License Photo is Processed Successfully")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "ProcessDriverLicensePhotoUpload")]
    [Trait("Description", "Ensures that 'SuccessAsync' is called on the outcome handler exactly once when the driver license photo is processed successfully")]
    public async Task SuccessAsync_MustBeInvoked_When_DriverLicensePhotoIsProcessedSuccessfully()
    {
        // Arrange
        var inbound = _fixture.Create<ProcessDriverLicensePhotoUploadInbound>();

        _repository
            .Setup(x => x.UpdateDriverLicensePhotoPathAsync(inbound.DeliveryDriverId, inbound.PhotoPath, default))
            .ReturnsAsync(1);

        _outcomeHandler.Setup(x => x.SuccessAsync(inbound.DeliveryDriverId, default)).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound);

        // Assert
        _outcomeHandler.Verify(handler => handler.SuccessAsync(inbound.DeliveryDriverId, default), Times.Once);
    }

    [Fact(DisplayName = "Outcome Handler DeliveryDriverNotFoundAsync Must Be Invoked When the Delivery Driver is Not Found")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "ProcessDriverLicensePhotoUpload")]
    [Trait("Description", "Ensures that 'DeliveryDriverNotFoundAsync' is called on the outcome handler exactly once when the delivery driver is not found")]
    public async Task DeliveryDriverNotFoundAsync_MustBeInvoked_When_DeliveryDriverIsNotFound()
    {
        // Arrange
        var inbound = _fixture.Create<ProcessDriverLicensePhotoUploadInbound>();

        _repository
            .Setup(x => x.UpdateDriverLicensePhotoPathAsync(inbound.DeliveryDriverId, inbound.PhotoPath, default))
            .ReturnsAsync(0);

        _outcomeHandler.Setup(x => x.DeliveryDriverNotFoundAsync(inbound.DeliveryDriverId, default)).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound);

        // Assert
        _outcomeHandler.Verify(handler => handler.DeliveryDriverNotFoundAsync(inbound.DeliveryDriverId, default), Times.Once);
    }
}
