using Adapters.Inbound.DeliveryDriverHttpApiAdapter.Modules.Common;

using Core.Application.Common;
using Core.Application.UseCases.RegisterDeliveryDriver.Inbounds;
using Core.Application.UseCases.RegisterMotorcycle.Inbounds;

using Microsoft.AspNetCore.Mvc;

namespace Adapters.Inbound.DeliveryDriverHttpApiAdapter.Controllers.RegisterDeliveryDriver.V1;


/// <summary>
/// Represents the controller for the delivery driver management endpoints.
/// </summary>
[ApiController]
[Route("api/v1/delivery-drivers")]
[Produces("application/json")]
[Consumes("application/json")]
public sealed class DeliveryDriverController(ILogger<DeliveryDriverController> logger)
    : ControllerBase, IRegisterDeliveryDriverOutcomeHandler
{
    private readonly ILogger<DeliveryDriverController> _logger = logger;

    private IResult? _viewModel;

    void IRegisterDeliveryDriverOutcomeHandler.Duplicated()
    {
        var message = "The delivery driver registration could not be completed because the provided CNPJ or driver's license number is already in use.";
        var response = ApiResponse<ProblemDetails>.CreateDuplicateResourceError(HttpContext, message);
        _viewModel = Results.Conflict(response);
    }

    void IRegisterDeliveryDriverOutcomeHandler.Invalid(IDictionary<string, string[]> errors)
    {
        var message = "The delivery driver registration could not be completed because the provided data is invalid.";
        var response = ApiResponse<ValidationProblemDetails>.CreateValidationError(errors, HttpContext, message);
        _viewModel = Results.BadRequest(response);
    }

    void IRegisterDeliveryDriverOutcomeHandler.Registered(Guid deliveryDriverId, string presignedUrl)
    {
        var message = "The delivery driver was successfully registered.";
        var response = ApiResponse<DeliveryDriverRegistrationResponse>.CreateSuccess(new DeliveryDriverRegistrationResponse(deliveryDriverId, presignedUrl), message);
        _viewModel = Results.Created(presignedUrl, response);
    }

    /// <summary>
    /// Registers a new delivery driver.
    /// </summary>
    /// <param name="useCase">The use case to register a delivery driver.</param>
    /// <param name="request">The request to register a delivery driver.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The result of the delivery driver registration.</returns>
    /// <response code="201">The delivery driver was successfully registered.</response>
    /// <response code="400">The request to register the delivery driver is invalid.</response>
    /// <response code="409">The delivery driver could not be registered because the provided CNPJ or driver's license number is already in use.</response>
    /// <remarks>
    /// This endpoint is used to register a new delivery driver in the system. The request must contain the necessary data
    /// to register the delivery driver, such as the name, CNPJ, date of birth, driver's license number, and driver's license type.
    /// </remarks>
    /// <example>
    /// POST /api/v1/delivery-drivers
    /// {
    ///  "name": "John Doe",
    ///  "cnpj": "12345678901234",
    ///  "dateOfBirth": "1980-01-01",
    ///  "driverLicenseNumber": "1234567890",
    ///  "driverLicenseCategory": "A"
    ///  }
    ///  </example>
    [HttpPost(Name = "RegisterDeliveryDriver")]
    [ProducesResponseType(typeof(ApiResponse<DeliveryDriverRegistrationResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<ValidationProblemDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<ProblemDetails>), StatusCodes.Status409Conflict)]
    public async Task<IResult> RegisterDeliveryDriverAsync(
        [FromKeyedServices(UseCaseType.Validation)] IRegisterDeliveryDriverUseCase useCase,
        [FromBody] RegisterDeliveryDriverRequest request,
        CancellationToken cancellationToken)
    {
        useCase.SetOutcomeHandler(this);

        var inbound = new RegisterDeliveryDriverInbound(
            Guid.NewGuid(), request.Name, request.Cnpj, request.DateOfBirth, request.DriverLicenseNumber, request.DriverLicenseCategory);

        await useCase.ExecuteAsync(inbound, cancellationToken);

        return _viewModel!;
    }
}
