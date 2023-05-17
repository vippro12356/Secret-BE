using Microsoft.EntityFrameworkCore;
using Secrets_Sharing_BE.Interfaces.Repositories;

namespace Secrets_Sharing_BE
{
    public interface IUnitOfWork
    {
        DbContext Context { get; }       
        IFileDataRepository FileDataRepository { get; }
        ITextDataRepository TextDataRepository { get; }
        IUserRepository UserRepository { get; }
        void Save();
    }
}
