using Core.Application.Common;
using Core.Application.UseCases.RegisterMotorcycle.Inbounds;

using Microsoft.AspNetCore.Mvc;

namespace Adapters.Inbound.HttpApiAdapter.Controllers.RegisterMotorcycles.V1;


/// <summary>
/// Controller for managing motorcycles.
/// </summary>
[ApiController]
[Route("api/v1/motorcycles")]
[Produces("application/json")]
public sealed class MotorcyclesController(ILogger<MotorcyclesController> logger)
    : ControllerBase, IMotorcycleRegistrationOutcomeHandler
{
    private readonly ILogger<MotorcyclesController> _logger = logger;

    private IResult? _viewModel;

    void IMotorcycleRegistrationOutcomeHandler.Duplicated()
    {
        var problemDetails = new ValidationProblemDetails()
        {
            Type = "https://tools.ietf.org/html/rfc9110#section-15.5.10",
            Title = "One or more model validation errors occurred.",
            Status = StatusCodes.Status400BadRequest,
            Detail = "See the errors property for details.",
            Instance = HttpContext.Request.Path
        };

        problemDetails.Extensions.Add("traceId", HttpContext.TraceIdentifier);

        _viewModel = Results.Conflict(problemDetails);
    }

    void IMotorcycleRegistrationOutcomeHandler.Invalid(IDictionary<string, string[]> errors)
    {
        var problemDetails = new ValidationProblemDetails(errors)
        {
            Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
            Title = "One or more model validation errors occurred.",
            Status = StatusCodes.Status400BadRequest,
            Detail = "See the errors property for details.",
            Instance = HttpContext.Request.Path
        };

        problemDetails.Extensions.Add("traceId", HttpContext.TraceIdentifier);

        _viewModel = Results.BadRequest(problemDetails);
    }

    void IMotorcycleRegistrationOutcomeHandler.Registered(Guid motorcycleId)
    {
        _viewModel = Results.CreatedAtRoute(
            routeName: "GetMotorcycleDetailsById",
            routeValues: new { id = motorcycleId }
        );
    }

    /// <summary>
    /// Registers a new motorcycle in the system.
    /// </summary>
    /// <param name="useCase"></param>
    /// <param name="request">The motorcycle registration request.</param>
    /// <returns>A response indicating the result of the registration operation.</returns>
    /// <response code="201">Indicates that the motorcycle was successfully registered.</response>
    /// <response code="400">Indicates that the request was invalid, such as missing required fields or invalid data format.</response>
    /// <response code="409">Indicates a conflict, such as when a motorcycle with the same license plate already exists.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IResult> RegisterMotorcycle(
        [FromKeyedServices(UseCaseType.Validation)] IMotorcycleRegistrationProcessor useCase,
        [FromBody] MotorcycleRegistrationRequest request)
    {
        useCase.SetOutcomeHandler(this);

        var inbound = new MotorcycleRegistrationInbound(Guid.NewGuid(), request.Year, request.Model, request.LicensePlate);

        await useCase.ExecuteAsync(inbound);

        return _viewModel!;
    }
}
