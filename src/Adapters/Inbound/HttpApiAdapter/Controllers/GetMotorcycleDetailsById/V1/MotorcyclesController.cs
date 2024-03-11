using Core.Application.Common;
using Core.Application.UseCases.RegisterMotorcycle.Inbounds;

using Microsoft.AspNetCore.Mvc;

namespace Adapters.Inbound.HttpApiAdapter.Controllers.GetMotorcycleDetailsById.V1;


/// <summary>
/// Controller for managing motorcycles.
/// </summary>
[ApiController]
[Route("api/v1/motorcycles")]
[Produces("application/json")]
public sealed class MotorcyclesController() : ControllerBase
{
    [HttpGet("{id:guid}", Name = "GetMotorcycleDetailsById")]
    public IResult GetDetails(Guid id)
    {
        return Results.NoContent();
    }
}
