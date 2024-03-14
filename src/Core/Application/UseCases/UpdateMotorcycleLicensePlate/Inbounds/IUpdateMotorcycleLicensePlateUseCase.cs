namespace Core.Application.UseCases.UpdateMotorcycleLicensePlate.Inbounds;

/// <summary>
/// Defines the contract for the use case responsible for updating the license plate of a motorcycle.
/// </summary>
public interface IUpdateMotorcycleLicensePlateUseCase
{
    /// <summary>
    /// Executes the update operation for a motorcycle's license plate.
    /// </summary>
    /// <param name="inbound">Data for updating the motorcycle license plate.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task ExecuteAsync(UpdateMotorcycleLicensePlateInbound inbound);

    /// <summary>
    /// Sets the handler that will be used to process the outcomes of the update operation.
    /// </summary>
    /// <param name="outcomeHandler">The handler responsible for processing the outcomes, providing a way to respond to different operation results such as success, validation errors, and not found situations.</param>
    void SetOutcomeHandler(IUpdateMotorcycleLicensePlateOutcomeHandler outcomeHandler);
}
