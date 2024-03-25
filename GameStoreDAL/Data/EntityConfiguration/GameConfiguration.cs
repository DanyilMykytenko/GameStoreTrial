using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfiguration;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Key).IsRequired();
        builder.HasIndex(x => x.Key).IsUnique();

        builder.Property(x => x.Price).IsRequired();

        builder.Property(x => x.Name).IsRequired();

        builder.Property(x => x.Viewed).HasDefaultValue(1);

        builder
            .HasMany(x => x.Genres)
            .WithMany(x => x.Games);
        builder
            .HasMany(x => x.Platforms)
            .WithMany(x => x.Games);
        builder
            .HasOne(x => x.Publisher)
            .WithMany(x => x.Games)
            .HasForeignKey(x => x.PublisherId)
            .HasPrincipalKey(x => x.Id);
        builder
            .HasMany(x => x.Comments)
            .WithOne(x => x.Game)
            .HasForeignKey(x => x.GameId);
    }
}
