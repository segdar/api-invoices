namespace ApiInvoices.Application.DTOs
{
    public class ApiResponse<T>
    {
        public string Status { get; set; } = string.Empty; // ok / error
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static ApiResponse<T> Ok(T data, string message = "success")
            => new() { Status = "ok", Message = message, Data = data };

        public static ApiResponse<T?> Error(string message)
            => new() { Status = "error", Message = message, Data = default };
    }
}
