using Secrets_Sharing_BE.Models;

namespace Secrets_Sharing_BE.Interfaces.Repositories
{
    public interface ITextDataRepository:IRepository<TextData>
    {
        TextData? GetData(string id);
        List<TextData> GetTextByUser(int id);
    }
}
