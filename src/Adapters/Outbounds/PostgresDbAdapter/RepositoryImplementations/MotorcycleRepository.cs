using Core.Application.UseCases.RegisterMotorcycle.Outbounds;
using Core.Domain;

using Dapper;

using Npgsql;

namespace Adapters.Outbounds.PostgresDbAdapter.RepositoryImplementations;

public class MotorcycleRepository(string connectionString) : IMotorcycleRepository
{
    private readonly string _connectionString = connectionString;

    public async Task<bool> ExistsByLicensePlateAsync(string licensePlate)
    {
        return await Task.FromResult(false);
    }

    public async Task RegisterAsync(Motorcycle motorcycle)
    {
        var sql = @"
                INSERT INTO motorcycle
                    (""motorcycle_id"", ""year"", ""model"", ""license_plate"", ""created_at"", ""update_at"")
                VALUES
                    (@MotorcycleId, @Year, @Model, @LicensePlate, @CreatedAt, @UpdatedAt)";

        using var connection = new NpgsqlConnection(_connectionString);

        await connection.ExecuteAsync(sql, motorcycle);
    }
}
