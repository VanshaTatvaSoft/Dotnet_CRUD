using System.Net;

namespace Web_Api_Service.DTO;

public class ApiResponse<T>
{
    public bool Success { get; set; } = true;
    public HttpStatusCode StatusCode { get; set; }
    public string SuccessMessage { get; set; }
    public T Data { get; set; }

    public ApiResponse(HttpStatusCode statusCode, string successMessage, bool success = true, T data = default)
    {
        Success = success;
        StatusCode = statusCode;
        SuccessMessage = successMessage;
        Data = data;
    }
}
