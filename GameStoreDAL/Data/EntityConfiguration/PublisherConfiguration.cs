using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfiguration
{
    public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CompanyName).IsRequired();
            builder.Property(x => x.CompanyName).HasMaxLength(40);
            builder.HasIndex(x => x.CompanyName).IsUnique();
        }
    }
}
