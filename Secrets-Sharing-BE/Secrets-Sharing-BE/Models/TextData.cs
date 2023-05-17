using System.ComponentModel.DataAnnotations.Schema;

namespace Secrets_Sharing_BE.Models
{
    public class TextData:BaseEntity
    {
        public string Message { get; set; } = null!;
        public bool AutoDelete { get; set; }
        public int UploadBy { get; set; }
        public string Protect { get; set; } = null!;
        public User User { get; set; } = null!; 
    }
}
