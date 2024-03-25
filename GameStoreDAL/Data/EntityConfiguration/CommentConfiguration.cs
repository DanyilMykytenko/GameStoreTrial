using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfiguration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Body).IsRequired();


            builder
                .HasOne(x => x.Parent)
                .WithMany(x => x.Nested)
                .HasForeignKey(x => x.ParentId);
        }
    }
}
