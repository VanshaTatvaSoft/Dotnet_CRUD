using System.Net;

namespace Web_Api_Service.DTO;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public string SuccessMessage { get; set; }
    public T Data { get; set; }

    public ApiResponse(bool success, HttpStatusCode statusCode, string successMessage, T data = default)
    {
        Success = success;
        StatusCode = statusCode;
        SuccessMessage = successMessage;
        Data = data;
    }

    public static ApiResponse<T> SuccessResponse(T data, HttpStatusCode statusCode = HttpStatusCode.OK, string message = "Request successful")
    {
        return new ApiResponse<T>(true, statusCode, message, data);
    }

    public static ApiResponse<T> FailResponse(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ApiResponse<T>(false, statusCode, message);
    }
}
