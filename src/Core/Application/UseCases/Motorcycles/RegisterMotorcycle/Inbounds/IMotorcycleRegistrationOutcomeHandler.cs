namespace MotoDeliveryManager.Core.Application.UseCases.Motorcycles.RegisterMotorcycle.Inbounds;

/// <summary>
/// Defines the contract for handling the outcomes of the motorcycle registration process.
/// </summary>
public interface IMotorcycleRegistrationOutcomeHandler
{
    /// <summary>
    /// Handles the scenario where a motorcycle with the same license plate already exists.
    /// </summary>
    /// <param name="licensePlate">The existing motorcycle license plate.</param>
    void Duplicated(string licensePlate);

    /// <summary>
    /// Handles the scenario where the motorcycle registration data is invalid.
    /// </summary>
    void Invalid(IDictionary<string, string[]> errors);

    /// <summary>
    /// Handles the successful registration of a motorcycle.
    /// </summary>
    /// <param name="motorcycleId">The unique identifier of the registered motorcycle.</param>
    void Registered(Guid motorcycleId);
}
