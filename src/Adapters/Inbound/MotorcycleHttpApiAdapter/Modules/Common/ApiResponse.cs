using Microsoft.AspNetCore.Mvc;

namespace Adapters.Inbound.MotorcycleHttpApiAdapter.Modules.Common;

/// <summary>
/// Encapsulates a standardized response format for API operations, offering a consistent structure for both successful outcomes and errors.
/// </summary>
/// <typeparam name="T">Type of the data included in the response for successful operations or error details for failed operations.</typeparam>
public class ApiResponse<T>
{
    /// <summary>
    /// Indicates whether the operation was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Provides a message summarizing the outcome of the operation.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Contains the operation's data payload for successful requests or error details for failed requests.
    /// </summary>
    public T Data { get; set; }

    // Private constructor ensures that instances are created through the provided static factory methods.
    private ApiResponse(bool success, string message, T data)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    /// <summary>
    /// Generates a success response containing the specified data payload.
    /// </summary>
    /// <param name="data">The data to include in the response.</param>
    /// <param name="message">Optional message describing the successful operation. Defaults to "Operation successful."</param>
    /// <returns>An ApiResponse instance encapsulating the successful operation's details.</returns>
    public static ApiResponse<T> CreateSuccess(T data, string message = "Operation successful.")
    {
        return new ApiResponse<T>(true, message, data);
    }

    /// <summary>
    /// Produces a response for situations where an operation fails due to the attempted creation or modification of a resource that already exists.
    /// </summary>
    /// <param name="context">The HttpContext of the current request, providing context for the error response.</param>
    /// <param name="message">Explanation of the error. Defaults to "Duplicate resource error."</param>
    /// <param name="detailUrl">An optional URL pointing to the resource that caused the duplicate error.</param>
    /// <returns>An ApiResponse object designed for duplicate resource error conditions.</returns>
    public static ApiResponse<ProblemDetails> CreateDuplicateResourceError(
        HttpContext context,
        string? detailUrl,
        string message = "Duplicate resource error.")
    {
        var problemDetails = new ProblemDetails
        {
            Title = "Duplicate Resource",
            Status = StatusCodes.Status409Conflict,
            Detail = message,
            Instance = context.Request.Path
        };

        if (!string.IsNullOrEmpty(detailUrl))
        {
            problemDetails.Extensions.Add("detailUrl", detailUrl);
        }

        problemDetails.Extensions.Add("traceId", context.TraceIdentifier);

        return new ApiResponse<ProblemDetails>(false, message, problemDetails);
    }

    /// <summary>
    /// Creates a response for a "Not Found" scenario, typically when a requested resource cannot be located.
    /// </summary>
    /// <param name="context">The current HttpContext, providing context for enriching the response.</param>
    /// <param name="message">A custom message explaining the not found error. Defaults to a general 'Resource not found' message if not specified.</param>
    /// <returns>An ApiResponse object designed for not found error conditions.</returns>
    public static ApiResponse<ProblemDetails> CreateNotFoundResponse(HttpContext context, string message = "The requested resource was not found.")
    {
        var problemDetails = new ProblemDetails
        {
            Title = "Resource Not Found",
            Status = StatusCodes.Status404NotFound,
            Detail = message,
            Instance = context.Request.Path
        };

        problemDetails.Extensions.Add("traceId", context.TraceIdentifier);

        return new ApiResponse<ProblemDetails>(false, message, problemDetails);
    }

    /// <summary>
    /// Constructs a response detailing a validation error encountered during an operation.
    /// </summary>
    /// <param name="errors">A dictionary mapping field names to validation error messages.</param>
    /// <param name="context">The HttpContext of the current request, used to enrich the response with request-specific details.</param>
    /// <param name="message">Custom message explaining the error condition. Defaults to "Validation errors occurred."</param>
    /// <returns>An ApiResponse object tailored for validation error scenarios.</returns>
    public static ApiResponse<ValidationProblemDetails> CreateValidationError(
        IDictionary<string, string[]> errors,
        HttpContext context,
        string message = "Validation errors occurred.")
    {
        var problemDetails = new ValidationProblemDetails(errors)
        {
            Title = "Validation Error",
            Status = StatusCodes.Status400BadRequest,
            Detail = message,
            Instance = context.Request.Path
        };

        problemDetails.Extensions.Add("traceId", context.TraceIdentifier);

        return new ApiResponse<ValidationProblemDetails>(false, message, problemDetails);
    }
}
