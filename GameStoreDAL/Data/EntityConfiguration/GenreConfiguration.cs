using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfiguration;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x => x.Name).IsRequired();

        builder
            .HasOne(x => x.Parent)
            .WithMany(x => x.Nested)
            .HasForeignKey(x => x.ParentId);

        builder.Property(x => x.ParentId).IsRequired(false);
    }
}
