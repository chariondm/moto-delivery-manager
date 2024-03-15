using Core.Domain.Motorcycles;

namespace Core.Application.UseCases.UpdateMotorcycleLicensePlate.Inbounds;

/// <summary>
/// Handles the outcomes of attempting to update a motorcycle's license plate.
/// </summary>
public interface IUpdateMotorcycleLicensePlateOutcomeHandler
{
    /// <summary>
    /// Invoked when the update operation is successful.
    /// </summary>
    /// <param name="motorcycle">The updated motorcycle entity.</param>
    void Success(Motorcycle motorcycle);

    /// <summary>
    /// Invoked when the input data for the update operation is invalid.
    /// </summary>
    /// <param name="errors">A collection of validation errors.</param>
    void Invalid(IDictionary<string, string[]> errors);

    /// <summary>
    /// Invoked when the new license plate provided already exists in the system for another motorcycle.
    /// <param name="licensePlate">The existing motorcycle license plate.</param>
    /// </summary>
    void DuplicateLicensePlate(string licensePlate);

    /// <summary>
    /// Invoked when the motorcycle to be updated could not be found.
    /// </summary>
    void MotorcycleNotFound();
}
