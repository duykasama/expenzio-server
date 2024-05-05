namespace Expenzio.Domain.Models.Responses;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }

    public ApiResponse(bool success, int statusCode, string message, T data)
    {
        Success = success;
        StatusCode = statusCode;
        Message = message;
        Data = data;
    }
}

public class ApiResponse : ApiResponse<object?>
{
    public ApiResponse(bool success, int statusCode, string message) : base(success, statusCode, message, null)
    {
    }

    public ApiResponse(bool success, int statusCode, string message, object data) : base(success, statusCode, message, data)
    {
    }
}
