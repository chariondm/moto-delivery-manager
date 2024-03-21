using Amazon.SQS;
using Amazon.SQS.Model;

namespace Adapters.Inbound.SQSDriverLicensePhotoProcessorAdapter;

public class DriverLicensePhotoProcessorWorker(
    IAmazonSQS sqsClient,
    string queueUrl,
    ILogger<DriverLicensePhotoProcessorWorker> logger) : BackgroundService
{
    private readonly IAmazonSQS _sqsClient = sqsClient;
    private readonly string _queueUrl = queueUrl;
    private readonly ILogger<DriverLicensePhotoProcessorWorker> _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var request = new ReceiveMessageRequest
            {
                QueueUrl = _queueUrl,
                MaxNumberOfMessages = 10,
                WaitTimeSeconds = 20
            };

            var response = await _sqsClient.ReceiveMessageAsync(request, stoppingToken);

            if (response.Messages.Count == 0)
            {
                _logger.LogWarning("No messages received");

                continue;
            }

            foreach (var message in response.Messages)
            {
                _logger.LogInformation("Processing message {MessageId}", message.MessageId);

                // Process the message

                var deleteRequest = new DeleteMessageRequest
                {
                    QueueUrl = _queueUrl,
                    ReceiptHandle = message.ReceiptHandle
                };

                await _sqsClient.DeleteMessageAsync(deleteRequest, stoppingToken);
            }

        }
    }
}
