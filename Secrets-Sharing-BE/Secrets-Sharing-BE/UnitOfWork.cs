using Microsoft.EntityFrameworkCore;
using Secrets_Sharing_BE.Interfaces.Repositories;

namespace Secrets_Sharing_BE
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; set; }   
        public ITextDataRepository TextDataRepository { get; set; }
        public IFileDataRepository FileDataRepository { get; set; }
        public IUserRepository UserRepository { get; }
        public UnitOfWork(Context context,            
            ITextDataRepository textDataRepository,
            IFileDataRepository fileDataRepository,
            IUserRepository userRepository) 
        {
            Context = context;           
            TextDataRepository = textDataRepository;
            FileDataRepository = fileDataRepository;
            UserRepository = userRepository;
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
