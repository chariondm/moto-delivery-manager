namespace MotoDeliveryManager.Core.Application.UseCases.DeliveryDrivers.RegisterDeliveryDriver.Inbounds;

/// <summary>
/// The use case for registering a delivery driver.
/// </summary>
public interface IRegisterDeliveryDriverUseCase
{
    /// <summary>
    /// Executes the registration of a delivery driver with the given inbound data.
    /// </summary>
    /// <param name="inbound">The inbound data for the registration.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <remarks>
    /// This method is responsible for executing the registration of a delivery driver with the given inbound data.
    /// </remarks>
    /// <seealso cref="RegisterDeliveryDriverInbound"/>
    Task ExecuteAsync(RegisterDeliveryDriverInbound inbound, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the outcome handler for the use case.
    /// </summary>
    /// <param name="outcomeHandler">The outcome handler to set.</param>
    /// <remarks>
    /// This method is responsible for setting the outcome handler for the use case.
    /// </remarks>
    /// <seealso cref="IRegisterDeliveryDriverOutcomeHandler"/>
    void SetOutcomeHandler(IRegisterDeliveryDriverOutcomeHandler outcomeHandler);
}
