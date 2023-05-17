using Secrets_Sharing_BE.Interfaces.Repositories;
using Secrets_Sharing_BE.Models;

namespace Secrets_Sharing_BE.Repositories
{
    public class FileDataRepository : Repository<FileData>, IFileDataRepository
    {
        public FileDataRepository(Context context) : base(context)
        {
        }
        public FileData? GetFile(string id)
        {
            return Dbset.FirstOrDefault(t => t.Protect == id);
        }
        public List<FileData> GetFiles(int id)
        {
            return Dbset.Where(t=>t.UploadBy==id).ToList();
        }
    }
}
