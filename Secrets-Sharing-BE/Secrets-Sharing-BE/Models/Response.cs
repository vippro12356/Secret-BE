namespace Secrets_Sharing_BE.Models
{
    public class Response
    {
        public string Message { get; set; } = null!;
    }
    public class ErrorResponse : Response
    {

    }
    public class SuccessResponse<T> : Response
    {
        public T? Data { get; set; }
    }
    public class FileResponse
    {
        public byte[]? Data { get; set; }
        public string? ContentType { get; set; }
    }
    public class LoginResponse
    {
        public string access_token { get; set; } = null!;
        public int expires_in { get; set; }
        public string token_type { get; set; } = null!;
        public string refresh_token { get; set; } = null!;
        public string scope { get; set; } = null!;
    }
}
