namespace MotoDeliveryManager.Adapters.Outbounds.PostgresDbAdapter.Entities.DeliveryDrivers
{
    public class DeliveryDriverRepository(IDbConnectionFactory connectionFactory, ILogger<DeliveryDriverRepository> logger)
        : IProcessDriverLicensePhotoUploadRepository, IRegisterDeliveryDriverRepository
    {
        private readonly IDbConnectionFactory _connectionFactory = connectionFactory;
        private readonly ILogger<DeliveryDriverRepository> _logger = logger;

        public async Task<bool> IsCnpjOrDriverLicenseNumberInUseAsync(
            string cnpj,
            string driverLicenseNumber,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogDebug("Checking if the CNPJ or driver's license number is already in use.");

                var parameters = new { Cnpj = cnpj, DriverLicenseNumber = driverLicenseNumber };

                var sql = @"
            SELECT EXISTS
                (
                    SELECT
                        1 
                    FROM
                        delivery_driver
                    WHERE 
                        cnpj = @Cnpj
                    OR
                        driver_license_number = @DriverLicenseNumber
                )";

                var command = new CommandDefinition(sql, parameters, cancellationToken: cancellationToken);

                using var connection = await _connectionFactory.CreateConnectionAsync(cancellationToken);

                return await connection.QueryFirstOrDefaultAsync<bool>(command);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while checking if the CNPJ or driver's license number is already in use.");
                throw;
            }
        }

        public async Task RegisterDeliveryDriverAsync(DeliveryDriver deliveryDriver,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogDebug("Registering a new delivery driver.");

                var parameters = new
                {
                    deliveryDriver.Id,
                    deliveryDriver.Name,
                    deliveryDriver.Cnpj,
                    deliveryDriver.DateOfBirth,
                    DriverLicenseNumber = deliveryDriver.DriverLicense.Number,
                    DriverLicenseCategory = deliveryDriver.DriverLicense.Category.ToString(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                var sql = @"
            INSERT INTO delivery_driver
                (delivery_driver_id, name, cnpj, date_of_birth, driver_license_number, driver_license_category, created_at, updated_at)
            VALUES
                (@Id, @Name, @Cnpj, @DateOfBirth, @DriverLicenseNumber, @DriverLicenseCategory, @CreatedAt, @UpdatedAt)";

                var command = new CommandDefinition(sql, parameters, cancellationToken: cancellationToken);

                using var connection = await _connectionFactory.CreateConnectionAsync(cancellationToken);

                await connection.ExecuteAsync(command);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while registering a new delivery driver.");
                throw;
            }
        }

        public async Task<int> UpdateDriverLicensePhotoPathAsync(
            Guid deliveryDriverId,
            string photoPath,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogDebug("Updating the driver's license photo path.");

                var parameters = new
                {
                    DeliveryDriverId = deliveryDriverId,
                    PhotoPath = photoPath,
                    UpdatedAt = DateTime.UtcNow
                };

                var sql = @"
            UPDATE
                delivery_driver
            SET
                driver_license_photo_path = @PhotoPath,
                updated_at = @UpdatedAt
            WHERE
                delivery_driver_id = @DeliveryDriverId";

                var command = new CommandDefinition(sql, parameters, cancellationToken: cancellationToken);

                using var connection = await _connectionFactory.CreateConnectionAsync(cancellationToken);

                return await connection.ExecuteAsync(command);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the driver's license photo path.");
                throw;
            }
        }
    }
}
