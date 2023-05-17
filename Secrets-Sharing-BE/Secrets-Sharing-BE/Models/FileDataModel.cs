namespace Secrets_Sharing_BE.Models
{
    public class FileDataModel
    {
        public bool AutoDelete { get; set; }
        public IFormFile File { get; set; } = null!;
    }
}
