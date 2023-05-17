using Secrets_Sharing_BE.Interfaces.Repositories;
using Secrets_Sharing_BE.Models;

namespace Secrets_Sharing_BE.Repositories
{
    public class TextDataRepository : Repository<TextData>, ITextDataRepository
    {
        public TextDataRepository(Context context) : base(context)
        {

        }

        public TextData? GetData(string id)
        {
            return Dbset.FirstOrDefault(t => t.Protect == id);
        }
        public List<TextData> GetTextByUser(int id)
        {
            return Dbset.Where(t=>t.UploadBy== id).ToList();
        }
    }
}
