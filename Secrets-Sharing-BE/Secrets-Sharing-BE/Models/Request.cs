namespace Secrets_Sharing_BE.Models
{
    public class Request
    {
        public string Message { get; set; } = null!;
        public bool AutoDelete { get; set; } = false;
    }
}
