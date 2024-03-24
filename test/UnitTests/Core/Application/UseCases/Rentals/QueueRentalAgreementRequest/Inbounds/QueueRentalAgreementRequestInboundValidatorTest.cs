namespace MotoDeliveryManager.UnitTests.Core.Application.UseCases.Rentals.QueueRentalAgreementRequest.Inbounds;

public class QueueRentalAgreementRequestInboundValidatorTests
{
    private readonly IFixture _fixture;
    private readonly QueueRentalAgreementRequestInboundValidator _sut;

    public QueueRentalAgreementRequestInboundValidatorTests()
    {
        _fixture = CustomFixture.CreateFixture();

        _sut = new QueueRentalAgreementRequestInboundValidator();
    }

    [Fact(DisplayName = "Rental Agreement Id Must Not Be Empty")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "QueueRentalAgreementRequest")]
    [Trait("Description", "Ensures that the rental agreement id must not be empty.")]
    public void RentalAgreementId_MustNotBeEmpty()
    {
        // Arrange
        var inbound = _fixture.Build<QueueRentalAgreementRequestInbound>()
            .With(x => x.RentalAgreementId, Guid.Empty)
            .With(x => x.ExpectedReturnDate, DateOnly.FromDateTime(DateTime.Today.AddDays(1)))
            .Create();

        // Act
        var result = _sut.Validate(inbound);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(x => x.ErrorMessage == "'Rental Agreement Id' must not be empty.");
        result.Errors.Should().ContainSingle(x => x.PropertyName == nameof(inbound.RentalAgreementId));
    }

    [Fact(DisplayName = "Delivery Driver Id Must Not Be Empty")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "QueueRentalAgreementRequest")]
    [Trait("Description", "Ensures that the delivery driver id must not be empty.")]
    public void DeliveryDriverId_MustNotBeEmpty()
    {
        // Arrange
        var inbound = _fixture.Build<QueueRentalAgreementRequestInbound>()
            .With(x => x.DeliveryDriverId, Guid.Empty)
            .With(x => x.ExpectedReturnDate, DateOnly.FromDateTime(DateTime.Today.AddDays(1)))
            .Create();

        // Act
        var result = _sut.Validate(inbound);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(x => x.ErrorMessage == "'Delivery Driver Id' must not be empty.");
        result.Errors.Should().ContainSingle(x => x.PropertyName == nameof(inbound.DeliveryDriverId));
    }

    [Fact(DisplayName = "Rental Plan Id Must Not Be Empty")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "QueueRentalAgreementRequest")]
    [Trait("Description", "Ensures that the rental plan id must not be empty.")]
    public void RentalPlanId_MustNotBeEmpty()
    {
        // Arrange
        var inbound = _fixture.Build<QueueRentalAgreementRequestInbound>()
            .With(x => x.RentalPlanId, Guid.Empty)
            .With(x => x.ExpectedReturnDate, DateOnly.FromDateTime(DateTime.Today.AddDays(1)))
            .Create();

        // Act
        var result = _sut.Validate(inbound);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(x => x.ErrorMessage == "'Rental Plan Id' must not be empty.");
        result.Errors.Should().ContainSingle(x => x.PropertyName == nameof(inbound.RentalPlanId));
    }

    [Fact(DisplayName = "Expected Return Date Must Be Greater Than Today")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "QueueRentalAgreementRequest")]
    [Trait("Description", "Ensures that the expected return date must be greater than today.")]
    public void ExpectedReturnDate_MustBeGreaterThanToday()
    {
        // Arrange
        var today = DateOnly.FromDateTime(DateTime.Today);
        var inbound = _fixture.Build<QueueRentalAgreementRequestInbound>()
            .With(x => x.ExpectedReturnDate, today)
            .Create();

        // Act
        var result = _sut.Validate(inbound);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(x => x.ErrorMessage == $"'Expected Return Date' must be greater than '{today}'.");
        result.Errors.Should().ContainSingle(x => x.PropertyName == nameof(inbound.ExpectedReturnDate));
    }

    [Fact(DisplayName = "Expected Return Date Must Not Be Empty")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "QueueRentalAgreementRequest")]
    [Trait("Description", "Ensures that the expected return date must not be empty.")]
    public void ExpectedReturnDate_MustNotBeEmpty()
    {
        // Arrange
        var inbound = _fixture.Build<QueueRentalAgreementRequestInbound>()
            .With(x => x.ExpectedReturnDate, DateOnly.MinValue)
            .Create();

        // Act
        var result = _sut.Validate(inbound);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(x => x.ErrorMessage == "'Expected Return Date' must not be empty.");
        result.Errors.Should().Contain(x => x.PropertyName == nameof(inbound.ExpectedReturnDate));
    }

    [Fact(DisplayName = "Inbound Must Be Valid")]
    [Trait("Category", "Unit Test")]
    [Trait("UseCase", "QueueRentalAgreementRequest")]
    [Trait("Description", "Ensures that the inbound must be valid.")]
    public void Inbound_MustBeValid()
    {
        // Arrange
        var inbound = _fixture.Build<QueueRentalAgreementRequestInbound>()
            .With(x => x.ExpectedReturnDate, DateOnly.FromDateTime(DateTime.Today.AddDays(1)))
            .Create();

        // Act
        var result = _sut.Validate(inbound);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }
}
