using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Secrets_Sharing_BE.Models;

namespace Secrets_Sharing_BE.Configurations
{
    public class TextDataConfiguration : IEntityTypeConfiguration<TextData>
    {
        public void Configure(EntityTypeBuilder<TextData> builder)
        {
            builder.ToTable(nameof(TextData));
            builder.HasKey(x => x.Id);
            builder.HasAlternateKey(x => x.Protect);
            builder.HasOne(t=>t.User).WithMany(t=>t.TextDatas).HasForeignKey(t=>t.UploadBy).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
