namespace MotoDeliveryManager.Core.Application.UseCases.Rentals.ListRentalPlans.Inbounds;

/// <summary>
/// Represents the use case for listing motorcycle rental plans.
/// </summary>
/// <remarks>It is used to list all available rental plans for motorcycles.</remarks>
/// <seealso cref="IListRentalPlansOutcomeHandler"/>
/// <seealso cref="ListRentalPlansUseCase"/>
public interface IListRentalPlansUseCase
{
    /// <summary>
    /// Executes the use case for listing motorcycle rental plans.
    /// </summary>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <remarks>It is used to list all available rental plans for motorcycles.</remarks>
    /// <seealso cref="ListRentalPlansUseCase"/>
    Task ExecuteAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Sets the outcome handler for the processing operation.
    /// </summary>
    /// <remarks>It is used to notify the caller of the outcome of the use case.</remarks>
    /// <param name="outcomeHandler">The outcome handler to set.</param>
    /// <seealso cref="IListRentalPlansOutcomeHandler"/>
    void SetOutcomeHandler(IListRentalPlansOutcomeHandler outcomeHandler);
}
