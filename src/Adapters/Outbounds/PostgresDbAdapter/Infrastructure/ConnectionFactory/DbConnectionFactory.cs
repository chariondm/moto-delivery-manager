using System.Data;

using Npgsql;

namespace Adapters.Outbounds.PostgresDbAdapter.Infrastructure.ConnectionFactory;

/// <summary>
/// Represents the database connection factory.
/// </summary>
/// <remarks>
/// This class is used to create a new asynchronous database connection to the PostgreSQL database.
/// </remarks>
/// <seealso cref="IDbConnectionFactory" />
/// <seealso cref="IDbConnection" />
/// <seealso cref="NpgsqlConnection" />
public class DbConnectionFactory(string connectionString) : IDbConnectionFactory
{
    private readonly string _connectionString = connectionString;

    public async Task<IDbConnection> CreateConnectionAsync(CancellationToken cancellationToken = default)
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        return connection;
    }
}