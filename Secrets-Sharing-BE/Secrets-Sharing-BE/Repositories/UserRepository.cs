using Secrets_Sharing_BE.Interfaces.Repositories;
using Secrets_Sharing_BE.Models;

namespace Secrets_Sharing_BE.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(Context context) : base(context)
        {

        }
        public User? GetUserByEmail(string email)
        {
            return Dbset.FirstOrDefault(t => t.Email == email);
        }
    }
}
