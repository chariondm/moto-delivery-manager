namespace MotoDeliveryManager.Adapters.Outbounds.PostgresDbAdapter.Infrastructure.ConnectionFactory
{
    /// <summary>
    /// Represents the contract for the database connection factory.
    /// </summary>
    /// <seealso cref="IDbConnectionFactory" />
    /// <seealso cref="IDbConnection" />
    public interface IDbConnectionFactory
    {
        /// <summary>
        /// Creates a new database connection.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A new database connection.</returns>
        Task<IDbConnection> CreateConnectionAsync(CancellationToken cancellationToken = default);
    }
}