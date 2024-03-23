namespace MotoDeliveryManager.Adapters.Inbound.DeliveryDriverHttpApiAdapter.Controllers.Rentals.ListRentalPlans.V1;

/// <summary>
/// Represents the controller for the rental management endpoints.
/// </summary>
/// <remarks>It is used to list all available rental plans for motorcycles.</remarks>
/// <seealso cref="IListRentalPlansOutcomeHandler"/>
/// <seealso cref="IListRentalPlansUseCase"/>
[ApiController]
[Route("api/v1/rentals")]
[Produces("application/json")]
[Consumes("application/json")]
public sealed class RentalController(ILogger<RentalController> logger)
    : ControllerBase, IListRentalPlansOutcomeHandler
{
    private readonly ILogger<RentalController> _logger = logger;

    private IResult? _viewModel;

    void IListRentalPlansOutcomeHandler.FoundRentalPlans(IEnumerable<RentalPlan> rentalPlans)
    {
        var message = "The rental plans were successfully found.";
        var rentalPlanResponses = rentalPlans.Select(ListRentalPlansResponse.ConvertFromRentalPlan);
        var response = ApiResponse<IEnumerable<ListRentalPlansResponse>>.CreateSuccess(rentalPlanResponses, message);
        _viewModel = Results.Ok(response);
    }

    void IListRentalPlansOutcomeHandler.NotFoundRentalPlans()
    {
        var message = "No rental plans were found.";
        var response = ApiResponse<ProblemDetails>.CreateNotFoundResponse(HttpContext, message);
        _viewModel = Results.NotFound(response);
    }

    /// <summary>
    /// Lists all available rental plans.
    /// </summary>
    /// <param name="useCase">The use case to list rental plans.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The result of the rental plans listing.</returns>
    /// <response code="200">The rental plans were successfully found.</response>
    /// <response code="404">No rental plans were found.</response>
    /// <remarks>
    /// This endpoint is used to list all available rental plans in the system.
    /// </remarks>
    /// <example>
    /// GET /api/v1/rentals
    /// </example>
    [HttpGet("rental-plans", Name = "ListRentalPlans")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ListRentalPlansResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ProblemDetails>), StatusCodes.Status404NotFound)]
    public async Task<IResult> ListRentalPlansAsync(
        [FromServices] IListRentalPlansUseCase useCase,
        CancellationToken cancellationToken)
    {
        useCase.SetOutcomeHandler(this);

        await useCase.ExecuteAsync(cancellationToken);

        return _viewModel!;
    }
}
