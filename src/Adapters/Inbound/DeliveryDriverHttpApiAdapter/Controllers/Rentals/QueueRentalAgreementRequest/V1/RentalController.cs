namespace MotoDeliveryManager.Adapters.Inbound.DeliveryDriverHttpApiAdapter.Controllers.Rentals.QueueRentalAgreementRequest.V1;

/// <summary>
/// Represents the controller for the rental management endpoints.
/// </summary>
/// <remarks>It is used to queue a rental agreement request for a motorcycle.</remarks>
/// <seealso cref="IQueueRentalAgreementRequestOutcomeHandler"/>
/// <seealso cref="IQueueRentalAgreementRequestUseCase"/>
[ApiController]
[Route("api/v1/rentals")]
[Produces("application/json")]
[Consumes("application/json")]
public sealed class RentalController(ILogger<RentalController> logger)
    : ControllerBase, IQueueRentalAgreementRequestOutcomeHandler
{
    private readonly ILogger<RentalController> _logger = logger;

    private IResult? _viewModel;

    void IQueueRentalAgreementRequestOutcomeHandler.RentalAgreementRequestNotQueued(IDictionary<string, string[]> errors)
    {
        var message = "The rental agreement request was not queued.";
        var response = ApiResponse<ProblemDetails>.CreateValidationError(errors, HttpContext, message);
        _viewModel = Results.UnprocessableEntity(response);
    }

    void IQueueRentalAgreementRequestOutcomeHandler.RentalAgreementRequestNotValid(IDictionary<string, string[]> errors)
    {
        var message = "The rental agreement request was not valid.";
        var response = ApiResponse<ProblemDetails>.CreateValidationError(errors, HttpContext, message);
        _viewModel = Results.BadRequest(response);
    }

    void IQueueRentalAgreementRequestOutcomeHandler.RentalAgreementRequestQueued(Guid rentalAgreementId)
    {
        var message = "The rental agreement request was successfully queued.";
        var rentalAgreementResponse = new QueueRentalAgreementRequestResponse(rentalAgreementId);
        var response = ApiResponse<QueueRentalAgreementRequestResponse>.CreateSuccess(rentalAgreementResponse, message);
        _viewModel = Results.Accepted(string.Empty, response);
    }

    /// <summary>
    /// Queues a rental agreement request.
    /// </summary>
    /// <param name="request">The request to queue a rental agreement.</param>
    /// <param name="useCase">The use case to queue a rental agreement request.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The result of the rental agreement request queuing.</returns>
    /// <response code="202">The rental agreement request was successfully queued.</response>
    /// <response code="400">The rental agreement request was not valid.</response>
    /// <response code="422">The rental agreement request was not queued.</response>
    /// <remarks>It is used to queue a rental agreement request for a motorcycle.</remarks>
    /// <seealso cref="QueueRentalAgreementRequestRequest"/>
    /// <seealso cref="QueueRentalAgreementRequestResponse"/>
    /// <seealso cref="IQueueRentalAgreementRequestUseCase"/>
    /// <seealso cref="IQueueRentalAgreementRequestOutcomeHandler"/>
    /// <example>
    /// POST /api/v1/rentals/agreements/queue
    /// {
    ///    "deliveryDriverId": "b3f3b3b3-3b3b-3b3b-3b3b-3b3b3b3b3b3b",
    ///    "rentalPlanId": "b3f3b3b3-3b3b-3b3b-3b3b-3b3b3b3b3b3b",
    ///    "expectedReturnDate: "2022-12-31"
    /// }
    /// </example>
    [HttpPost("agreements/queue", Name = "QueueRentalAgreementRequest")]
    [ProducesResponseType(typeof(ApiResponse<QueueRentalAgreementRequestResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ApiResponse<ProblemDetails>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<ProblemDetails>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IResult> QueueRentalAgreementRequestAsync(
        [FromBody] QueueRentalAgreementRequestRequest request,
        [FromKeyedServices(UseCaseType.Validation)] IQueueRentalAgreementRequestUseCase useCase,
        CancellationToken cancellationToken)
    {
        useCase.SetOutcomeHandler(this);

        var inbound = new QueueRentalAgreementRequestInbound(
            Guid.NewGuid(),
            request.DeliveryDriverId,
            request.RentalPlanId,
            request.ExpectedReturnDate);

        await useCase.ExecuteAsync(inbound, cancellationToken);

        return _viewModel!;
    }
}
