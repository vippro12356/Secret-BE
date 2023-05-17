using Secrets_Sharing_BE.Models;

namespace Secrets_Sharing_BE.Interfaces.Services
{
    public interface ITextDataService
    {
        TextData PrepUpload(Request data, out string error, int userid);
    }
}
