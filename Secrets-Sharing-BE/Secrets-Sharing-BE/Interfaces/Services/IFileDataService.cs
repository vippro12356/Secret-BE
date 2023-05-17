using Secrets_Sharing_BE.Models;

namespace Secrets_Sharing_BE.Interfaces.Services
{
    public interface IFileDataService
    {
        FileData PrepCreate(IFormFile file, bool delete, out string error, int userid);
        void Upload(IFormFile file, out string message);
        FileResponse? Download(string filename);
        void DeleteFile(string filename, out string message);
    }
}
