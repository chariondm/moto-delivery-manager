using Adapters.Inbound.HttpApiAdapter.Modules.Common;

using Core.Application.Common;
using Core.Application.UseCases.UpdateMotorcycleLicensePlate.Inbounds;
using Core.Domain;

using Microsoft.AspNetCore.Mvc;

namespace Adapters.Inbound.HttpApiAdapter.Controllers.UpdateMotorcycleLicensePlates.V1;

/// <summary>
/// Controller for managing motorcycles.
/// </summary>
[ApiController]
[Route("api/v1/motorcycles")]
[Produces("application/json")]
public sealed class MotorcyclesController(ILogger<MotorcyclesController> logger)
    : ControllerBase, IUpdateMotorcycleLicensePlateOutcomeHandler
{
    private readonly ILogger<MotorcyclesController> _logger = logger;

    private IResult? _viewModel;

    void IUpdateMotorcycleLicensePlateOutcomeHandler.Success(Motorcycle motorcycle)
    {
        var response = ApiResponse<Motorcycle>.CreateSuccess(motorcycle, "Motorcycle License Plate updated successfully.");
        _viewModel = Results.Ok(response);
    }

    void IUpdateMotorcycleLicensePlateOutcomeHandler.DuplicateLicensePlate(string licensePlate)
    {
        var url = Url.Link("FilterMotorcyclesByLicensePlate", new { licensePlate });
        var message = "The provided license plate already exists. Please use a different license plate.";
        var response = ApiResponse<ProblemDetails>.CreateDuplicateResourceError(HttpContext, message, url);
        _viewModel = Results.Conflict(response);
    }

    void IUpdateMotorcycleLicensePlateOutcomeHandler.MotorcycleNotFound()
    {
        var message = "The specified motorcycle was not found.";
        var response = ApiResponse<ProblemDetails>.CreateNotFoundResponse(HttpContext, message);
        _viewModel = Results.NotFound(response);
    }

    void IUpdateMotorcycleLicensePlateOutcomeHandler.Invalid(IDictionary<string, string[]> errors)
    {
        var message = "Validation failed for the motorcycle data provided.";
        var response = ApiResponse<ValidationProblemDetails>.CreateValidationError(errors, HttpContext, message);
        _viewModel = Results.BadRequest(response);
    }

    /// <summary>
    /// Updates a motorcycle's license plate.
    /// </summary>
    /// <param name="useCase">The use case for updating motorcycle license plate.</param>
    /// <param name="motorcycleId">The unique identifier of the motorcycle.</param>
    /// <param name="request">The new license plate for the motorcycle.</param>
    /// <returns>A response indicating the outcome of the update operation.</returns>
    /// <response code="200">Indicates that the motorcycle's license plate was successfully updated.</response>
    /// <response code="400">Indicates that the request was invalid, such as missing required fields or invalid data format.</response>
    /// <response code="404">Indicates that the specified motorcycle was not found.</response>
    /// <response code="409">Indicates a conflict, such as when a motorcycle with the same license plate already exists.</response>
    [HttpPatch("{motorcycleId}/license-plate", Name = "UpdateMotorcycleLicensePlate")]
    [ProducesResponseType(typeof(ApiResponse<Motorcycle>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ValidationProblemDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<ProblemDetails>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<ProblemDetails>), StatusCodes.Status409Conflict)]
    public async Task<IResult> UpdateMotorcycleLicensePlateAsync(
        [FromKeyedServices(UseCaseType.Validation)] IUpdateMotorcycleLicensePlateUseCase useCase,
        [FromRoute] Guid motorcycleId,
        [FromBody] UpdateMotorcycleLicensePlatesRequest request)
    {
        useCase.SetOutcomeHandler(this);

        var inbound = new UpdateMotorcycleLicensePlateInbound(motorcycleId, request.LicensePlate);

        await useCase.ExecuteAsync(inbound);

        return _viewModel!;
    }
}
