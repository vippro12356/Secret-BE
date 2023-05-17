using Secrets_Sharing_BE.Interfaces.Services;
using Secrets_Sharing_BE.Models;

namespace Secrets_Sharing_BE.Services
{
    public class TextDataService : ITextDataService
    {
        public TextData PrepUpload(Request data, out string error,int userid)
        {
            error = string.Empty;
            if(string.IsNullOrEmpty(data.Message))
            {
                error = "Message cannot be null";
            }
            TextData dt=new TextData()
            {
                Message= data.Message,
                AutoDelete = data.AutoDelete,
                UploadBy= userid,
                Protect = Guid.NewGuid().ToString("N")//this string will protect the url
            };
            return dt;
        }
    }
}
