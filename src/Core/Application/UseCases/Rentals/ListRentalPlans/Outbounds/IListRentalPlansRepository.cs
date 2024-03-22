using Core.Domain.Rentals;

namespace Core.Application.UseCases.Rentals.ListRentalPlans.Outbounds;

public interface IListRentalPlansRepository
{
    /// <summary>
    /// Lists all available motorcycle rental plans.
    /// </summary>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of rental plans.</returns>
    /// <remarks>It is used to list all available rental plans for motorcycles.</remarks>
    /// <seealso cref="RentalPlan"/>
    Task<IEnumerable<RentalPlan>> ListRentalPlansAsync(CancellationToken cancellationToken);
}
