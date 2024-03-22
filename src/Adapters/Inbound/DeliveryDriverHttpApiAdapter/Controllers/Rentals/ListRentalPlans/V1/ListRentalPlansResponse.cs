using Core.Domain.Rentals;

namespace Adapters.Inbound.DeliveryDriverHttpApiAdapter.Controllers.Rentals.ListRentalPlans.V1;

/// <summary>
/// Represents the response for listing motorcycle rental plans.
/// </summary>
/// <param name="Id">The unique identifier of the rental plan.</param>
/// <param name="DurationDays">The duration of the rental plan in days.</param>
/// <param name="DailyCost">The daily cost of the rental plan.</param>
/// <param name="TotalCost">The total cost of the rental plan.</param>
/// <remarks>It is used to describe the terms of a rental plan.</remarks>
public record ListRentalPlansResponse(Guid Id, int DurationDays, decimal DailyCost, decimal TotalCost)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ListRentalPlansResponse"/> from the specified <paramref name="rentalPlan"/>.
    /// </summary>
    /// <param name="rentalPlan">The rental plan to create the response from.</param>
    /// <returns>The response created from the specified <paramref name="rentalPlan"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the specified <paramref name="rentalPlan"/> is <c>null</c>.</exception>
    /// <remarks>It is used to create a response from a rental plan.</remarks>
    /// <seealso cref="RentalPlan"/>
    public static ListRentalPlansResponse ConvertFromRentalPlan(RentalPlan rentalPlan)
        => new(rentalPlan.RentalPlanId, rentalPlan.DurationDays, rentalPlan.DailyCost, rentalPlan.CalculateTotalCost());
}
