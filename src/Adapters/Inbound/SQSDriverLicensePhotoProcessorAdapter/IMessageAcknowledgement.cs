namespace MotoDeliveryManager.Adapters.Inbound.SQSDriverLicensePhotoProcessorAdapter;

/// <summary>
/// Represents a message acknowledgement.
/// </summary>
/// <remarks>
/// This interface is used by the inbound adapter to acknowledge the receipt of a message from the message broker.
/// </remarks>
public interface IMessageAcknowledgement
{
    /// <summary>
    /// Gets the correlation identifier of the message.
    /// </summary>
    string CorrelationId { get; }

    /// <summary>
    /// Acknowledges the receipt of a message from the message broker.
    /// </summary>
    Task AcknowledgeAsync(string correlationId, CancellationToken cancellationToken);

    /// <summary>
    /// Sets the correlation identifier of the message.
    /// </summary>
    void SetCorrelationId(string correlationId);
}
