using Core.Application.UseCases.FilterMotorcyclesByLicensePlate.Outbounds;
using Core.Application.UseCases.RegisterMotorcycle.Outbounds;
using Core.Domain;

using Dapper;

using Npgsql;

namespace Adapters.Outbounds.PostgresDbAdapter.RepositoryImplementations;

public class MotorcycleRepository(string connectionString) : IRegisterMotorcycleRepository, IFilterMotorcyclesByLicensePlateRepository
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
}
