namespace MotoDeliveryManager.Adapters.Outbounds.PostgresDbAdapter.Entities.Rentals
{
    public class RentalPlanRepository(IDbConnectionFactory connectionFactory, ILogger<RentalPlanRepository> logger)
        : IListRentalPlansRepository
    {
        private readonly IDbConnectionFactory _connectionFactory = connectionFactory;
        private readonly ILogger<RentalPlanRepository> _logger = logger;

        public async Task<IEnumerable<RentalPlan>> ListRentalPlansAsync(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogDebug("Listing rental plans.");

                var sql = @"
            SELECT
                rental_plan_id AS RentalPlanId,
                duration_days AS DurationDays,
                daily_cost AS DailyCost,
                penalty_percentage AS PenaltyPercentage,
                additional_daily_cost AS AdditionalDailyCost
            FROM
                rental_plan";

                var command = new CommandDefinition(sql, cancellationToken: cancellationToken);

                using var connection = await _connectionFactory.CreateConnectionAsync(cancellationToken);

                return await connection.QueryAsync<RentalPlan>(command);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while listing rental plans.");
                throw;
            }
        }
    }
}
