using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfiguration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role
                {
                    Id = "CD712C58-B6AF-4881-92BD-4D1B2F7A171B",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                },
                new Role
                {
                    Id = "C2972C01-B621-4D34-9A53-0072FFBC8784",
                    Name = "Manager",
                    NormalizedName = "MANAGER"
                },
                new Role
                {
                    Id = "F3C74A98-0709-4467-9A14-7EB18950725D",
                    Name = "Moderator",
                    NormalizedName = "MODERATOR"
                },
                new Role
                {
                    Id = "FE5A4BC6-A366-4AF3-8188-6F4300DAA5A6",
                    Name = "Publisher",
                    NormalizedName = "PUBLISHER"
                },
                new Role
                {
                    Id = "4CEDE834-14E8-4392-8DC7-ABA9E4DD4B71",
                    Name = "User",
                    NormalizedName = "USER"
                },
                new Role
                {
                    Id = "54DCC2BB-0145-441B-B148-5EB18A9BC34B",
                    Name = "Guest",
                    NormalizedName = "GUEST"
                },
                new Role
                {
                    Id = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    Name = "SuperAdministrator",
                    NormalizedName = "SUPERADMINISTRATOR"
                });
        }
    }
}
