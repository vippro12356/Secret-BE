using Secrets_Sharing_BE.Models;

namespace Secrets_Sharing_BE.Interfaces.Repositories
{
    public interface IUserRepository:IRepository<User>
    {
        User? GetUserByEmail(string email);
    }
}
