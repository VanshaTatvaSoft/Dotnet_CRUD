using System.Net;

namespace Web_Api_Service.DTO;

public record ApiResponse<T>
(
    HttpStatusCode StatusCode,
    string SuccessMessage,
    bool Success = true,
    T Data = default
);
