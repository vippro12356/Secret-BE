using Secrets_Sharing_BE.Models;

namespace Secrets_Sharing_BE.Interfaces.Repositories
{
    public interface IFileDataRepository:IRepository<FileData>
    {
        FileData? GetFile(string id);
        List<FileData> GetFiles(int id);
    }
}
