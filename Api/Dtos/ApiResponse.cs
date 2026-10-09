namespace ApiInvoices.Api.Dtos;

public class ApiResponse<T>
{
    public string Status { get; set; } = "ok";
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }

    public static ApiResponse<T> Ok(string message, T? data)
    {
        return new ApiResponse<T> { Status = "ok", Message = message, Data = data };
    }

    public static ApiResponse<T> Error(string message)
    {
        return new ApiResponse<T> { Status = "error", Message = message };
    }
}
