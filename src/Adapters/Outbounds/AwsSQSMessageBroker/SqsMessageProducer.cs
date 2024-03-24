namespace MotoDeliveryManager.Adapters.Outbounds.AwsSQSMessageBroker;

public class SqsMessageProducer(IAmazonSQS sqsClient, string queueUrl, ILogger<SqsMessageProducer> logger)
    : IQueueRentalAgreementRequestMessageBroker
{ 
    private const string SuccessMessage = "Rental Agreement Request with Id: {RentalAgreementRequestId} has been queued successfully. Message Id: {MessageId}, Queue Url: {QueueUrl}";

    private const string ErrorMessage = "An error occurred while sending a message to SQS queue: {QueueUrl}. AWS Error Code: {ErrorCode}, Error Message: {ErrorMessage}";

    private const string UnexpectedErrorMessage = "An unexpected error occurred while sending a message to SQS queue: {QueueUrl}";

    private readonly IAmazonSQS _sqsClient = sqsClient;

    private readonly string _queueUrl = queueUrl;

    public readonly ILogger<SqsMessageProducer> _logger = logger;

    public async Task QueueRentalAgreementRequestAsync(
        QueueRentalAgreementRequestInbound rentalAgreementRequest,
        CancellationToken cancellationToken)
    {
        try
        {
            var messageBody = JsonSerializer.Serialize(rentalAgreementRequest);

            var sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = _queueUrl,
                MessageBody = messageBody
            };

            var response = await _sqsClient.SendMessageAsync(sendMessageRequest);

            _logger.LogInformation(SuccessMessage, rentalAgreementRequest.RentalAgreementId, response.MessageId, _queueUrl);
        }
        catch (AmazonSQSException ex)
        {
            _logger.LogError(ex, ErrorMessage, _queueUrl, ex.ErrorCode, ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, UnexpectedErrorMessage, _queueUrl);
            throw;
        }
    }
}

