using Adapters.Inbound.HttpApiAdapter.Modules.Common;

using Core.Application.Common;
using Core.Application.UseCases.FilterMotorcyclesByLicensePlate.Inbounds;
using Core.Domain;

using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;


namespace Adapters.Inbound.HttpApiAdapter.Controllers.FilterMotorcyclesByLicensePlate.V1;


/// <summary>
/// Controller for managing motorcycles.
/// </summary>
[ApiController]
[Route("api/v1/motorcycles")]
[Produces("application/json")]
public sealed class MotorcyclesController(ILogger<MotorcyclesController> logger)
    : ControllerBase, IFilterMotorcyclesByLicensePlateOutcomeHandler
{
    private readonly ILogger<MotorcyclesController> _logger = logger;

    private IResult? _viewModel;

    void IFilterMotorcyclesByLicensePlateOutcomeHandler.Invalid(IDictionary<string, string[]> errors)
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

    void IFilterMotorcyclesByLicensePlateOutcomeHandler.OnMotorcyclesFound(IEnumerable<Motorcycle> motorcycles)
    {
        _viewModel = Results.Ok(new ApiResponse<IEnumerable<Motorcycle>>(motorcycles, "Motorcycles found successfully."));
    }

    void IFilterMotorcyclesByLicensePlateOutcomeHandler.OnMotorcyclesNotFound()
    {
        _viewModel = Results.Ok(new ApiResponse<IEnumerable<Motorcycle>>(Enumerable.Empty<Motorcycle>(), "No motorcycles found."));
    }

    /// <summary>
    /// Filters motorcycles by license plate. Returns all motorcycles if no license plate is specified.
    /// </summary>
    /// <param name="useCase">The use case for filtering motorcycles by license plate.</param>
    /// <param name="licensePlate">The license plate to filter by. This parameter is optional.</param>
    /// <returns>A response indicating the result of the filter operation.</returns>
    /// <response code="200">Successfully retrieved list of motorcycles, which may be empty if no matches are found.</response>
    /// <response code="400">Indicates that the request was invalid, for example, if validation errors occur.</response>
    [HttpGet(Name = "FilterMotorcyclesByLicensePlate")]
    [SwaggerOperation(Summary = "Filters motorcycles by license plate", Description = "Returns all motorcycles if no license plate is specified.")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<Motorcycle>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IResult> FilterMotorcyclesByLicensePlate(
        [FromKeyedServices(UseCaseType.Validation)] IFilterMotorcyclesByLicensePlateUseCase useCase,
        [FromQuery(Name = "licensePlate")] string? licensePlate)
    {
        useCase.SetOutcomeHandler(this);

        await useCase.ExecuteAsync(licensePlate);

        return _viewModel!;
    }
}
