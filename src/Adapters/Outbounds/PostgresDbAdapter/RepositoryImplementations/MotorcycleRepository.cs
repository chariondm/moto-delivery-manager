using Adapters.Outbounds.PostgresDbAdapter.Infrastructure.ConnectionFactory;

using Core.Application.UseCases.FilterMotorcyclesByLicensePlate.Outbounds;
using Core.Application.UseCases.RegisterMotorcycle.Outbounds;
using Core.Application.UseCases.UpdateMotorcycleLicensePlate.Outbounds;
using Core.Domain.Motorcycles;

using Dapper;

using Microsoft.Extensions.Logging;

namespace Adapters.Outbounds.PostgresDbAdapter.RepositoryImplementations;

/// <summary>
/// A repository implementation that encompasses operations related to the Motorcycle domain entity.
/// This class implements interfaces for registering, updating, and filtering motorcycles by license plate,
/// leveraging the Dapper micro ORM for database interactions.
///
/// Design Decision:
/// This repository is designed as a unified class for simplicity and to maintain cohesive operations related to the Motorcycle entity.
/// It aims to minimize duplication of database access logic and simplify dependency injection configurations.
///
/// Pros:
/// - Cohesion: Keeps all Motorcycle-related operations in a single place for easier maintenance and understanding.
/// - Reusability: Allows for the reuse of common logic such as checking for the existence of a license plate.
/// - Simplified Configuration: Reduces the number of implementations to register with the dependency injection container.
///
/// Cons:
/// - Single Responsibility Principle (SRP) Violation: The class might end up having multiple reasons to change, which goes against SRP.
/// - Increased Complexity: As the Motorcycle domain grows, this class could become large and complex, making it harder to maintain.
/// - Testability: Testing a class that implements multiple interfaces can become more complex, especially if the operations are tightly coupled or involve significant business logic.
///
/// Recommendation:
/// Should this class grow significantly in complexity or responsibilities, consider refactoring into smaller classes focused on specific areas of functionality.
/// This could involve separating the repository into distinct classes for registration, updating, and filtering operations, following the Principle of Interface Segregation (ISP) for more tailored interfaces.
/// </summary>
public class MotorcycleRepository(
    IDbConnectionFactory connectionFactory,
    ILogger<MotorcycleRepository> logger)
    : IFilterMotorcyclesByLicensePlateRepository, IRegisterMotorcycleRepository, IUpdateMotorcycleLicensePlateRepository
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;
    private readonly ILogger<MotorcycleRepository> _logger = logger;

    public async Task<bool> ExistsByLicensePlateAsync(
        string licensePlate,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Checking if a motorcycle with the specified license plate already exists.");

            var parameters = new { LicensePlate = licensePlate };

            var sql = @"
            SELECT EXISTS
                (
                    SELECT
                        1 
                    FROM
                        motorcycle
                    WHERE 
                        license_plate = @LicensePlate
                )";

            var command = new CommandDefinition(sql, parameters, cancellationToken: cancellationToken);

            using var connection = await _connectionFactory.CreateConnectionAsync(cancellationToken);

            return await connection.QueryFirstOrDefaultAsync<bool>(command);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while checking if a motorcycle with the specified license plate already exists.");
            throw;
        }
    }

    public async Task<IEnumerable<Motorcycle>> FindByLicensePlateAsync(
        string? licensePlate,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Retrieving motorcycles optionally filtered by license plate.");

            var sql = @"
            SELECT
                 motorcycle_id AS MotorcycleId,
                 year AS Year,
                 model AS Model,
                 license_plate AS LicensePlate,
                 created_at AS CreatedAt,
                 updated_at AS UpdatedAt
            FROM
                motorcycle";

            var parameters = new DynamicParameters();

            if (!string.IsNullOrWhiteSpace(licensePlate))
            {
                sql += " WHERE license_plate = @LicensePlate";
                parameters.Add("LicensePlate", licensePlate);
            }

            var command = new CommandDefinition(sql, parameters, cancellationToken: cancellationToken);

            using var connection = await _connectionFactory.CreateConnectionAsync(cancellationToken);

            return await connection.QueryAsync<Motorcycle>(sql, parameters);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving motorcycles optionally filtered by license plate.");
            throw;
        }
    }

    public async Task RegisterAsync(Motorcycle motorcycle, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Registering a new motorcycle.");

            var sql = @"
            INSERT INTO motorcycle
                (motorcycle_id, year, model, license_plate, created_at, updated_at)
            VALUES
                (@MotorcycleId, @Year, @Model, @LicensePlate, @CreatedAt, @UpdatedAt)";

            var command = new CommandDefinition(sql, motorcycle, cancellationToken: cancellationToken);

            using var connection = await _connectionFactory.CreateConnectionAsync(cancellationToken);

            await connection.ExecuteAsync(command);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while registering a new motorcycle.");
            throw;
        }
    }

    public async Task<Motorcycle?> UpdateAsync(
        Guid motorcycleId,
        string newLicensePlate,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogDebug("Updating the license plate of a motorcycle.");

            var parameters = new
            {
                MotorcycleId = motorcycleId,
                NewLicensePlate = newLicensePlate,
                UpdatedAt = DateTime.UtcNow
            };

            var sql = @"
            UPDATE
                motorcycle
            SET
                license_plate = @NewLicensePlate, 
                updated_at = @UpdatedAt
            WHERE
                motorcycle_id = @MotorcycleId
            RETURNING 
                motorcycle_id AS MotorcycleId,
                year AS Year,
                model AS Model,
                license_plate AS LicensePlate,
                created_at AS CreatedAt,
                updated_at AS UpdatedAt;";

            var command = new CommandDefinition(sql, parameters, cancellationToken: cancellationToken);

            using var connection = await _connectionFactory.CreateConnectionAsync(cancellationToken);

            return await connection.QuerySingleOrDefaultAsync<Motorcycle>(command);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the license plate of a motorcycle.");
            throw;
        }
    }
}
