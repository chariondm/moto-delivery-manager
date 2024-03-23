namespace MotoDeliveryManager.Core.Application.UseCases.Motorcycles.FilterMotorcyclesByLicensePlate.Inbounds;

/// <summary>
/// Handles the outcomes of attempting to filter motorcycles by license plate.
/// </summary>
public interface IFilterMotorcyclesByLicensePlateOutcomeHandler
{
    /// <summary>
    /// Invoked when the provided license plate is invalid or the filtering criteria are not met.
    /// </summary>
    /// <param name="errors">A collection of validation errors.</param>
    void Invalid(IDictionary<string, string[]> errors);

    /// <summary>
    /// Invoked when motorcycles have been successfully found matching the criteria.
    /// </summary>
    /// <param name="motorcycles">A list of motorcycles matching the search criteria.</param>
    void OnMotorcyclesFound(IEnumerable<Motorcycle> motorcycles);

    /// <summary>
    /// Invoked when no motorcycles match the provided criteria or when no criteria are provided,
    /// an empty list is a valid response indicating a successful query with no matches.
    /// </summary>
    void OnMotorcyclesNotFound();
}
