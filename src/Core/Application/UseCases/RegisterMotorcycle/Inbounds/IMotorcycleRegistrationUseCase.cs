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
    Task ExecuteAsync(MotorcycleRegistrationInbound inbound);

    /// <summary>
    /// Sets the outcome handler for processing the results of the motorcycle registration.
    /// </summary>
    /// <param name="outcomeHandler">The outcome handler to set.</param>
    void SetOutcomeHandler(IMotorcycleRegistrationOutcomeHandler outcomeHandler);
}
