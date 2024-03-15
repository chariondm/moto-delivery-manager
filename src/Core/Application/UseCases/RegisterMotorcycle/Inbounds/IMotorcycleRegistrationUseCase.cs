namespace Core.Application.UseCases.RegisterMotorcycle.Inbounds;

/// <summary>
/// UseCase the registration of motorcycles.
/// </summary>
public interface IMotorcycleRegistrationUseCase
{
    /// <summary>
    /// Executes the motorcycle registration process using the provided inbound registration data.
    /// </summary>
    /// <param name="inbound">The data for motorcycle registration.</param>
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the outcome of the registration process.</returns>
    /// <remarks>
    Task ExecuteAsync(MotorcycleRegistrationInbound inbound, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the outcome handler for processing the results of the motorcycle registration.
    /// </summary>
    /// <param name="outcomeHandler">The outcome handler to set.</param>
    void SetOutcomeHandler(IMotorcycleRegistrationOutcomeHandler outcomeHandler);
}
