namespace MotoDeliveryManager.Adapters.Inbound.SQSDriverLicensePhotoProcessorAdapter;

public class DriverLicensePhotoProcessorWorker(IServiceProvider serviceProvider, string queueName)
    : BackgroundService, IMessageAcknowledgement, IProcessDriverLicensePhotoUploadOutcomeHandler
{
    public string CorrelationId { get; private set; } = string.Empty;

    private readonly string _queueName = queueName;

    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly ReceiveMessageRequest _receiveMessageRequest = new()
    {
        QueueUrl = queueName,
        MaxNumberOfMessages = 10,
        WaitTimeSeconds = 20
    };

    private readonly ILogger<DriverLicensePhotoProcessorWorker> _logger = serviceProvider
        .GetRequiredService<ILogger<DriverLicensePhotoProcessorWorker>>();

    private readonly IAmazonSQS _sqsClient = serviceProvider.GetRequiredService<IAmazonSQS>();

    private readonly IProcessDriverLicensePhotoUploadUseCase _useCase = serviceProvider
        .GetRequiredKeyedService<IProcessDriverLicensePhotoUploadUseCase>(UseCaseType.Validation);

    async Task IProcessDriverLicensePhotoUploadOutcomeHandler.DeliveryDriverNotFoundAsync(
        Guid deliveryDriverId,
        CancellationToken cancellationToken)
    {
        _logger.LogWarning("Driver {DeliveryDriverId} not found", deliveryDriverId);

        await AcknowledgeAsync(CorrelationId, cancellationToken);
    }

    void IProcessDriverLicensePhotoUploadOutcomeHandler.Invalid(IDictionary<string, string[]> errors)
    {
        var errorsAsStr = string.Join(", ", errors.Select(x => $"{x.Key}: {string.Join(", ", x.Value)}"));
        _logger.LogError("One or more validation errors occurred: {Errors}", errorsAsStr);
    }

    async Task IProcessDriverLicensePhotoUploadOutcomeHandler.SuccessAsync(
        Guid deliveryDriverId,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Driver license photo processed for driver {DeliveryDriverId}", deliveryDriverId);

        await AcknowledgeAsync(CorrelationId, cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var response = await _sqsClient.ReceiveMessageAsync(_receiveMessageRequest, stoppingToken);

                await ProcessBatchSQSMessagesAsync(response, stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing messages");
            }
        }
    }

    private async Task ProcessBatchSQSMessagesAsync(ReceiveMessageResponse response, CancellationToken stoppingToken)
    {
        if (response.Messages.Count == 0)
        {
            _logger.LogWarning("No messages received");

            return;
        }

        foreach (var message in response.Messages)
        {
            _logger.LogInformation("Processing message {MessageId}", message.MessageId);

            _useCase.SetOutcomeHandler(this);

            SetCorrelationId(message.ReceiptHandle);

            var inbound = JsonSerializer.Deserialize<ProcessDriverLicensePhotoUploadInbound>(message.Body, JsonSerializerOptions)!;

            await _useCase.ExecuteAsync(inbound, stoppingToken);
        }
    }

    public async Task AcknowledgeAsync(string correlationId, CancellationToken cancellationToken)
    {
        var deleteRequest = new DeleteMessageRequest
        {
            QueueUrl = _queueName,
            ReceiptHandle = correlationId
        };

        await _sqsClient.DeleteMessageAsync(deleteRequest, cancellationToken);
    }

    public void SetCorrelationId(string correlationId)
    {
        CorrelationId = correlationId;
    }
}
