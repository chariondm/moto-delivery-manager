using Adapters.Inbound.MotorcycleHttpApiAdapter.Modules.Common;

using Core.Application.Common;
using Core.Application.UseCases.FilterMotorcyclesByLicensePlate.Inbounds;
using MotoDeliveryManager.Core.Domain.Motorcycles;

using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;


namespace Adapters.Inbound.MotorcycleHttpApiAdapter.Controllers.FilterMotorcyclesByLicensePlate.V1;


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
        _viewModel = Results.BadRequest(ApiResponse<ValidationProblemDetails>.CreateValidationError(errors, HttpContext));
    }

    void IFilterMotorcyclesByLicensePlateOutcomeHandler.OnMotorcyclesFound(IEnumerable<Motorcycle> motorcycles)
    {
        var response = ApiResponse<IEnumerable<Motorcycle>>.CreateSuccess(motorcycles, "Motorcycles found successfully.");
        _viewModel = Results.Ok(response);
    }

    void IFilterMotorcyclesByLicensePlateOutcomeHandler.OnMotorcyclesNotFound()
    {
        var response = ApiResponse<IEnumerable<Motorcycle>>.CreateSuccess([], "No motorcycles found.");
        _viewModel = Results.Ok(response);
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
    [ProducesResponseType(typeof(ApiResponse<ValidationProblemDetails>), StatusCodes.Status400BadRequest)]
    public async Task<IResult> FilterMotorcyclesByLicensePlate(
        [FromKeyedServices(UseCaseType.Validation)] IFilterMotorcyclesByLicensePlateUseCase useCase,
        [FromQuery(Name = "licensePlate")] string? licensePlate)
    {
        useCase.SetOutcomeHandler(this);

        await useCase.ExecuteAsync(licensePlate);

        return _viewModel!;
    }
}
