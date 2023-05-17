using Microsoft.EntityFrameworkCore;
using Secrets_Sharing_BE.Configurations;

namespace Secrets_Sharing_BE
{
    public class Context:DbContext
    {
        public Context(DbContextOptions op):base(op) 
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {          
            modelBuilder.ApplyConfiguration(new TextDataConfiguration());
            modelBuilder.ApplyConfiguration(new FileDataConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
