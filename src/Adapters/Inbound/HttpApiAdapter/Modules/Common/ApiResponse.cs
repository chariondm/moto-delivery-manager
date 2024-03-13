namespace Adapters.Inbound.HttpApiAdapter.Modules.Common;

/// <summary>
/// Represents a standardized response structure for API operations.
/// This class encapsulates the response data along with a success indicator and a message for the client.
/// </summary>
/// <typeparam name="T">The type of the data being returned in the response.</typeparam>
public class ApiResponse<T>
{
    /// <summary>
    /// Gets or sets a value indicating whether the API operation was successful.
    /// </summary>
    /// <value>True if the operation was successful; otherwise, false.</value>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets a message providing additional information about the API operation's outcome.
    /// This message can be used to convey success messages or additional details in case of success.
    /// </summary>
    /// <value>A string message related to the operation's outcome.</value>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets the data resulting from the API operation.
    /// This is the actual response payload expected by the client.
    /// </summary>
    /// <value>The data of type <typeparamref name="T"/> produced by the operation.</value>
    public T Data { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiResponse{T}"/> class with the specified data and optional message.
    /// </summary>
    /// <param name="data">The data being returned by the API operation.</param>
    /// <param name="message">An optional message about the operation's outcome. Defaults to an empty string.</param>
    public ApiResponse(T data, string message = "")
    {
        Success = true;
        Data = data;
        Message = message;
    }
}

