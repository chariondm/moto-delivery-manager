using Core.Application.UseCases.FilterMotorcyclesByLicensePlate.Outbounds;
using Core.Application.UseCases.RegisterMotorcycle.Outbounds;
using Core.Application.UseCases.UpdateMotorcycleLicensePlate.Outbounds;
using Core.Domain;

using Dapper;

using Npgsql;

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
public class MotorcycleRepository(string connectionString)
    : IFilterMotorcyclesByLicensePlateRepository, IRegisterMotorcycleRepository, IUpdateMotorcycleLicensePlateRepository
{
    private readonly string _connectionString = connectionString;

    public async Task<bool> ExistsByLicensePlateAsync(string licensePlate)
    {
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

        using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<bool>(sql, new { LicensePlate = licensePlate });
    }

    public async Task<IEnumerable<Motorcycle>> FindByLicensePlateAsync(string? licensePlate)
    {
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

        using var connection = new NpgsqlConnection(_connectionString);
        return await connection.QueryAsync<Motorcycle>(sql, parameters);
    }

    public async Task RegisterAsync(Motorcycle motorcycle)
    {
        var sql = @"
                INSERT INTO motorcycle
                    (motorcycle_id, year, model, license_plate, created_at, updated_at)
                VALUES
                    (@MotorcycleId, @Year, @Model, @LicensePlate, @CreatedAt, @UpdatedAt)";

        using var connection = new NpgsqlConnection(_connectionString);

        await connection.ExecuteAsync(sql, motorcycle);
    }

    public async Task<Motorcycle?> UpdateAsync(Guid motorcycleId, string newLicensePlate)
    {
        var parameters = new { 
            MotorcycleId = motorcycleId, 
            NewLicensePlate = newLicensePlate ,
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

        using var connection = new NpgsqlConnection(_connectionString);

        return await connection
            .QuerySingleOrDefaultAsync<Motorcycle>(sql, parameters);
    }
}
