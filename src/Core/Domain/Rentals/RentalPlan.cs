namespace MotoDeliveryManager.Core.Domain.Rentals;

/// <summary>
/// Represents a rental plan.
/// </summary>
/// <param name="RentalPlanId">The unique identifier of the rental plan.</param>
/// <param name="DurationDays">The duration of the rental plan in days.</param>
/// <param name="DailyCost">The daily cost of the rental plan.</param>
/// <param name="PenaltyPercentage">The penalty percentage of the rental plan.</param>
/// <param name="AdditionalDailyCost">The additional daily cost of the rental plan.</param>
/// <remarks>It is used to describe the terms of a rental plan.</remarks>
/// <seealso cref="Rental"/>
public record RentalPlan(
    Guid RentalPlanId,
    int DurationDays,
    decimal DailyCost,
    decimal PenaltyPercentage,
    decimal AdditionalDailyCost)
{
    /// <summary>
    /// Calculates the total cost of the rental plan.                                                                       
    /// </summary>
    /// <returns>The total cost of the rental plan.</returns>
    /// <remarks>
    /// The total cost is calculated as the sum of the daily cost of the rental plan multiplied by the duration of the rental plan.
    /// </remarks>
    /// <seealso cref="DailyCost"/>
    /// <seealso cref="DurationDays"/>
    public decimal CalculateTotalCost() => DailyCost * DurationDays;

    /// <summary>
    /// Calculates the penalty fee for the unused days of the rental plan.
    /// </summary>
    /// <param name="days">The number of days the rental plan was unused.</param>
    /// <returns>The penalty fee for the unused days.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the number of days is negative.</exception>
    /// <remarks>
    /// The penalty fee is calculated as a percentage of the daily cost of the rental plan.
    /// </remarks>
    /// <seealso cref="DailyCost"/>
    /// <seealso cref="PenaltyPercentage"/>
    public decimal CalculatePenaltyFee(int days)
    {
        if (days < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(days), "The number of days cannot be negative.");
        }

        return DailyCost * PenaltyPercentage * days;
    }

    /// <summary>
    /// Calculates the additional fee for the extra days of the rental plan.
    /// </summary>
    /// <param name="days">The number of extra days the rental plan was used.</param>
    /// <returns>The additional fee for the extra days.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the number of days is negative.</exception>
    /// <remarks>
    /// The additional fee is calculated as a fixed amount per extra day.
    /// </remarks>
    /// <seealso cref="AdditionalDailyCost"/>
    public decimal CalculateAdditionalFee(int days)
    {
        if (days < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(days), "The number of days cannot be negative.");
        }

        return AdditionalDailyCost * days;
    }
}
