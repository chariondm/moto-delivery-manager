using AutoFixture;

using MotoDeliveryManager.Core.Domain.Rentals;

using UnitTests.Helpers;

namespace UnitTests.Core.Domain.Rentals;

public class RentalPlanTests
{
    private readonly IFixture _fixture;
    private readonly RentalPlan _sut;

    public RentalPlanTests()
    {
        _fixture = CustomFixture.CreateFixture();

        _sut = _fixture.Create<RentalPlan>();
    }

    [Fact(DisplayName = "CalculateTotalCost Must Return the Correct Total Cost")]
    [Trait("Category", "Unit Test")]
    [Trait("Domain", "RentalPlan")]
    [Trait("Description", "Ensures that 'CalculateTotalCost' returns the correct total cost of the rental plan")]
    public void CalculateTotalCost_MustReturn_CorrectTotalCost()
    {
        // Arrange
        var expected = _sut.DailyCost * _sut.DurationDays;

        // Act
        var actual = _sut.CalculateTotalCost();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "CalculatePenaltyFee Must Return the Correct Penalty Fee")]
    [Trait("Category", "Unit Test")]
    [Trait("Domain", "RentalPlan")]
    [Trait("Description", "Ensures that 'CalculatePenaltyFee' returns the correct penalty fee for the unused days of the rental plan")]
    public void CalculatePenaltyFee_MustReturn_CorrectPenaltyFee()
    {
        // Arrange
        var days = _fixture.Create<int>();
        var expected = _sut.DailyCost * _sut.PenaltyPercentage * days;

        // Act
        var actual = _sut.CalculatePenaltyFee(days);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "CalculatePenaltyFee Must Throw ArgumentOutOfRangeException When Days Is Negative")]
    [Trait("Category", "Unit Test")]
    [Trait("Domain", "RentalPlan")]
    [Trait("Description", "Ensures that 'CalculatePenaltyFee' throws an 'ArgumentOutOfRangeException' when the number of days is negative")]
    public void CalculatePenaltyFee_MustThrowArgumentOutOfRangeException_WhenDaysIsNegative()
    {
        // Arrange
        var days = -_fixture.Create<int>();

        // Act
        void Act() => _sut.CalculatePenaltyFee(days);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(Act);
    }

    [Fact(DisplayName = "CalculateAdditionalFee Must Return the Correct Additional Fee")]
    [Trait("Category", "Unit Test")]
    [Trait("Domain", "RentalPlan")]
    [Trait("Description", "Ensures that 'CalculateAdditionalFee' returns the correct additional fee for the extra days of the rental plan")]
    public void CalculateAdditionalFee_MustReturn_CorrectAdditionalFee()
    {
        // Arrange
        var days = _fixture.Create<int>();
        var expected = _sut.AdditionalDailyCost * days;

        // Act
        var actual = _sut.CalculateAdditionalFee(days);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "CalculateAdditionalFee Must Throw ArgumentOutOfRangeException When Days Is Negative")]
    [Trait("Category", "Unit Test")]
    [Trait("Domain", "RentalPlan")]
    [Trait("Description", "Ensures that 'CalculateAdditionalFee' throws an 'ArgumentOutOfRangeException' when the number of days is negative")]
    public void CalculateAdditionalFee_MustThrowArgumentOutOfRangeException_WhenDaysIsNegative()
    {
        // Arrange
        var days = -_fixture.Create<int>();

        // Act
        void Act() => _sut.CalculateAdditionalFee(days);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(Act);
    }
}
