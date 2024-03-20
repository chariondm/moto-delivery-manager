using AutoFixture;

using Core.Application.UseCases.RegisterDeliveryDriver;
using Core.Application.UseCases.RegisterDeliveryDriver.Inbounds;
using Core.Application.UseCases.RegisterDeliveryDriver.Outbounds;
using Core.Domain.DeliveryDrivers;

using Moq;

using UnitTests.Helpers;

namespace UnitTests.Core.Application.UseCases.RegisterDeliveryDriver;

public class RegisterDeliveryDriverUseCaseTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IRegisterDeliveryDriverRepository> _repository;
    private readonly Mock<IRegisterDeliveryDriverStorageService> _storageService;
    private readonly Mock<IRegisterDeliveryDriverOutcomeHandler> _outcomeHandler;
    private readonly RegisterDeliveryDriverUseCase _sut;

    public RegisterDeliveryDriverUseCaseTests()
    {
        _fixture = CustomFixture.CreateFixture();

        _repository = _fixture.Freeze<Mock<IRegisterDeliveryDriverRepository>>();
        _storageService = _fixture.Freeze<Mock<IRegisterDeliveryDriverStorageService>>();
        _outcomeHandler = _fixture.Freeze<Mock<IRegisterDeliveryDriverOutcomeHandler>>();

        _sut = new RegisterDeliveryDriverUseCase(_repository.Object, _storageService.Object);
        _sut.SetOutcomeHandler(_outcomeHandler.Object);
    }

    [Fact(DisplayName = "Outcome Handler Registered Must Be Invoked When a new Delivery Driver Is Registered")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "RegisterDeliveryDriver")]
    [Trait("Description", "Ensures that 'Registered' is called on the outcome handler exactly once when a new delivery driver is registered.")]
    public async Task Registered_MustBeInvoked_When_DeliveryDriverIsRegistered()
    {
        // Arrange
        var inbound = _fixture.Create<RegisterDeliveryDriverInbound>();

        var deliveryDriver = new DeliveryDriver(inbound.DeliveryDriverId, inbound.Name, inbound.Cnpj, inbound.DateOfBirth,
            new DriverLicense(inbound.DriverLicenseNumber, inbound.DriverLicenseCategory, null));
        
        var url = _fixture.Create<string>();

        _repository.Setup(x => x.RegisterDeliveryDriverAsync(deliveryDriver, default)).Verifiable();

        _storageService.Setup(x => x.GeneratePresignedUrlToUploadDeliveryDriverLicensePhotoAsync(deliveryDriver.Id, default))
            .ReturnsAsync(url)
            .Verifiable();

        _outcomeHandler.Setup(x => x.Registered(deliveryDriver.Id, url)).Verifiable();

        // Act
        await _sut.ExecuteAsync(inbound);

        // Assert
        _outcomeHandler.Verify(handler => handler.Registered(deliveryDriver.Id, url), Times.Once);
    }
}
