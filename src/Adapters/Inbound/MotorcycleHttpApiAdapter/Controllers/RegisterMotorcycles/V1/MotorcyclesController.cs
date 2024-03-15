using Adapters.Inbound.MotorcycleHttpApiAdapter.Modules.Common;

using Core.Application.Common;
using Core.Application.UseCases.RegisterMotorcycle.Inbounds;

using Microsoft.AspNetCore.Mvc;

namespace Adapters.Inbound.MotorcycleHttpApiAdapter.Controllers.RegisterMotorcycles.V1;


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

    void IMotorcycleRegistrationOutcomeHandler.Duplicated(string licensePlate)
    {
        var url = Url.Link("FilterMotorcyclesByLicensePlate", new { licensePlate }) ?? string.Empty;
        var message = "The motorcycle registration could not be completed because the provided license plate already exists in our database.";
        var response = ApiResponse<ProblemDetails>.CreateDuplicateResourceError(HttpContext, message, url);
        _viewModel = Results.Conflict(response);
    }

    void IMotorcycleRegistrationOutcomeHandler.Invalid(IDictionary<string, string[]> errors)
    {   
        var message = "Validation failed for the motorcycle data provided.";
        var response = ApiResponse<ValidationProblemDetails>.CreateValidationError(errors, HttpContext, message);
        _viewModel = Results.BadRequest(response);
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
    /// <param name="cancellationToken">The cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A response indicating the result of the registration operation.</returns>
    /// <response code="201">Indicates that the motorcycle was successfully registered.</response>
    /// <response code="400">Indicates that the request was invalid, such as missing required fields or invalid data format.</response>
    /// <response code="409">Indicates a conflict, such as when a motorcycle with the same license plate already exists.</response>
    [HttpPost(Name = "RegisterMotorcycle")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<ValidationProblemDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<ProblemDetails>), StatusCodes.Status409Conflict)]
    public async Task<IResult> RegisterMotorcycleAsync(
        [FromKeyedServices(UseCaseType.Validation)] IMotorcycleRegistrationUseCase useCase,
        [FromBody] MotorcycleRegistrationRequest request,
        CancellationToken cancellationToken = default)
    {
        useCase.SetOutcomeHandler(this);

        var inbound = new MotorcycleRegistrationInbound(Guid.NewGuid(), request.Year, request.Model, request.LicensePlate);

        await useCase.ExecuteAsync(inbound, cancellationToken);

        return _viewModel!;
    }
}
