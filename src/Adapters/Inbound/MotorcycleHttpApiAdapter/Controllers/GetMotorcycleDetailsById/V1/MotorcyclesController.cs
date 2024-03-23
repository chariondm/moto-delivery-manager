namespace MotoDeliveryManager.Adapters.Inbound.MotorcycleHttpApiAdapter.Controllers.GetMotorcycleDetailsById.V1;

/// <summary>
/// Controller for managing motorcycles.
/// </summary>
[ApiController]
[Route("api/v1/motorcycles")]
[Produces("application/json")]
[Consumes("application/json")]
public sealed class MotorcyclesController() : ControllerBase
{
    /// <summary>
    /// Retrieves the details of a motorcycle by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the motorcycle.</param>
    /// <returns>The details of the motorcycle.</returns>
    /// <response code="200">Indicates that the motorcycle details were successfully retrieved.</response>
    /// <response code="404">Indicates that the motorcycle with the specified identifier was not found.</response>
    [HttpGet("{id:guid}", Name = "GetMotorcycleDetailsById")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IResult GetMotorcycleDetailsById(Guid id)
    {
        throw new NotImplementedException();
    }
}
