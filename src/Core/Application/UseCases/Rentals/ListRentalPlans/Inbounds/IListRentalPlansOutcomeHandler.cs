using MotoDeliveryManager.Core.Domain.Rentals;

namespace Core.Application.UseCases.Rentals.ListRentalPlans.Inbounds;

/// <summary>
/// Represents the handler for the outcome of the ListRentalPlans use case.
/// </summary>
/// <remarks>It is used to notify the caller of the outcome of the use case.</remarks>
/// <seealso cref="RentalPlan"/>
public interface IListRentalPlansOutcomeHandler
{
    /// <summary>
    /// Invoked when rental plans have been successfully found.
    /// </summary>
    /// <param name="rentalPlans">A list of rental plans.</param>
    /// <seealso cref="RentalPlan"/>
    void FoundRentalPlans(IEnumerable<RentalPlan> rentalPlans);

    /// <summary>
    /// Invoked when no rental plans are found.
    /// </summary>
    /// <remarks>It is a valid response indicating a successful query with no matches.</remarks>
    /// <seealso cref="RentalPlan"/>
    void NotFoundRentalPlans();
}
