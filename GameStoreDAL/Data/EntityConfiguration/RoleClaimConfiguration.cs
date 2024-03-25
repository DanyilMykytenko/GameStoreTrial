using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityConfiguration
{
    public class RoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
        {
            builder.HasData(
                //seed administrator claims
                new IdentityRoleClaim<string>
                {
                    Id = 1,
                    RoleId = "CD712C58-B6AF-4881-92BD-4D1B2F7A171B",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.RoleCreatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 2,
                    RoleId = "CD712C58-B6AF-4881-92BD-4D1B2F7A171B",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.RoleUpdatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 3,
                    RoleId = "CD712C58-B6AF-4881-92BD-4D1B2F7A171B",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.RoleDeletePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 4,
                    RoleId = "CD712C58-B6AF-4881-92BD-4D1B2F7A171B",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.RoleGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 5,
                    RoleId = "CD712C58-B6AF-4881-92BD-4D1B2F7A171B",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.RolePermissionGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 6,
                    RoleId = "CD712C58-B6AF-4881-92BD-4D1B2F7A171B",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.UserCreatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 7,
                    RoleId = "CD712C58-B6AF-4881-92BD-4D1B2F7A171B",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.UserUpdatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 8,
                    RoleId = "CD712C58-B6AF-4881-92BD-4D1B2F7A171B",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.UserDeletePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 9,
                    RoleId = "CD712C58-B6AF-4881-92BD-4D1B2F7A171B",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.UserGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 10,
                    RoleId = "CD712C58-B6AF-4881-92BD-4D1B2F7A171B",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.CommentCreatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 11,
                    RoleId = "CD712C58-B6AF-4881-92BD-4D1B2F7A171B",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.CommentDeletePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 12,
                    RoleId = "CD712C58-B6AF-4881-92BD-4D1B2F7A171B",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.CommentGetPermission
                },

                //seed manager claims
                new IdentityRoleClaim<string>
                {
                    Id = 13,
                    RoleId = "C2972C01-B621-4D34-9A53-0072FFBC8784",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.GameCreatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 14,
                    RoleId = "C2972C01-B621-4D34-9A53-0072FFBC8784",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.GameUpdatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 15,
                    RoleId = "C2972C01-B621-4D34-9A53-0072FFBC8784",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.GameDeletePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 16,
                    RoleId = "C2972C01-B621-4D34-9A53-0072FFBC8784",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.GameGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 17,
                    RoleId = "C2972C01-B621-4D34-9A53-0072FFBC8784",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.GenreCreatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 18,
                    RoleId = "C2972C01-B621-4D34-9A53-0072FFBC8784",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.GenreUpdatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 19,
                    RoleId = "C2972C01-B621-4D34-9A53-0072FFBC8784",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.GenreDeletePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 20,
                    RoleId = "C2972C01-B621-4D34-9A53-0072FFBC8784",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.GenreGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 21,
                    RoleId = "C2972C01-B621-4D34-9A53-0072FFBC8784",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.PlatformTypeCreatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 22,
                    RoleId = "C2972C01-B621-4D34-9A53-0072FFBC8784",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.PlatformTypeUpdatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 23,
                    RoleId = "C2972C01-B621-4D34-9A53-0072FFBC8784",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.PlatformTypeDeletePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 24,
                    RoleId = "C2972C01-B621-4D34-9A53-0072FFBC8784",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.PlatformTypeGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 25,
                    RoleId = "C2972C01-B621-4D34-9A53-0072FFBC8784",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.PublisherCreatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 26,
                    RoleId = "C2972C01-B621-4D34-9A53-0072FFBC8784",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.PublisherUpdatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 27,
                    RoleId = "C2972C01-B621-4D34-9A53-0072FFBC8784",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.PublisherDeletePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 28,
                    RoleId = "C2972C01-B621-4D34-9A53-0072FFBC8784",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.PublisherGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 29,
                    RoleId = "C2972C01-B621-4D34-9A53-0072FFBC8784",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.OrderCreatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 30,
                    RoleId = "C2972C01-B621-4D34-9A53-0072FFBC8784",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.OrderGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 31,
                    RoleId = "C2972C01-B621-4D34-9A53-0072FFBC8784",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.OrderUpdatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 32,
                    RoleId = "C2972C01-B621-4D34-9A53-0072FFBC8784",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.OrderDetailsUpdatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 33,
                    RoleId = "C2972C01-B621-4D34-9A53-0072FFBC8784",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.OrderDetailsDeletePermission
                },

                //seed moderator claims
                new IdentityRoleClaim<string>
                {
                    Id = 34,
                    RoleId = "F3C74A98-0709-4467-9A14-7EB18950725D",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.CommentCreatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 35,
                    RoleId = "F3C74A98-0709-4467-9A14-7EB18950725D",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.CommentDeletePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 36,
                    RoleId = "F3C74A98-0709-4467-9A14-7EB18950725D",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.CommentGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 37,
                    RoleId = "F3C74A98-0709-4467-9A14-7EB18950725D",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.OrderGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 38,
                    RoleId = "F3C74A98-0709-4467-9A14-7EB18950725D",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.GameGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 39,
                    RoleId = "F3C74A98-0709-4467-9A14-7EB18950725D",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.GenreGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 40,
                    RoleId = "F3C74A98-0709-4467-9A14-7EB18950725D",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.PlatformTypeGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 41,
                    RoleId = "F3C74A98-0709-4467-9A14-7EB18950725D",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.PublisherGetPermission
                },

                //seed user claims
                new IdentityRoleClaim<string>
                {
                    Id = 42,
                    RoleId = "4CEDE834-14E8-4392-8DC7-ABA9E4DD4B71",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.CommentGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 43,
                    RoleId = "4CEDE834-14E8-4392-8DC7-ABA9E4DD4B71",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.OrderGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 44,
                    RoleId = "4CEDE834-14E8-4392-8DC7-ABA9E4DD4B71",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.GameGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 45,
                    RoleId = "4CEDE834-14E8-4392-8DC7-ABA9E4DD4B71",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.GenreGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 46,
                    RoleId = "4CEDE834-14E8-4392-8DC7-ABA9E4DD4B71",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.PlatformTypeGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 47,
                    RoleId = "4CEDE834-14E8-4392-8DC7-ABA9E4DD4B71",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.PublisherGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 48,
                    RoleId = "4CEDE834-14E8-4392-8DC7-ABA9E4DD4B71",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.CommentCreatePermission
                },

                //seed guest claims
                new IdentityRoleClaim<string>
                {
                    Id = 49,
                    RoleId = "54DCC2BB-0145-441B-B148-5EB18A9BC34B",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.CommentCreatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 50,
                    RoleId = "54DCC2BB-0145-441B-B148-5EB18A9BC34B",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.CommentGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 51,
                    RoleId = "54DCC2BB-0145-441B-B148-5EB18A9BC34B",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.GameGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 52,
                    RoleId = "54DCC2BB-0145-441B-B148-5EB18A9BC34B",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.GenreGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 53,
                    RoleId = "54DCC2BB-0145-441B-B148-5EB18A9BC34B",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.PlatformTypeGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 54,
                    RoleId = "54DCC2BB-0145-441B-B148-5EB18A9BC34B",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.PublisherGetPermission
                },

                //seed publisher claims
                new IdentityRoleClaim<string>
                {
                    Id = 55,
                    RoleId = "FE5A4BC6-A366-4AF3-8188-6F4300DAA5A6",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.CommentCreatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 56,
                    RoleId = "FE5A4BC6-A366-4AF3-8188-6F4300DAA5A6",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.CommentGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 57,
                    RoleId = "FE5A4BC6-A366-4AF3-8188-6F4300DAA5A6",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.OrderGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 58,
                    RoleId = "FE5A4BC6-A366-4AF3-8188-6F4300DAA5A6",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.GameGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 59,
                    RoleId = "FE5A4BC6-A366-4AF3-8188-6F4300DAA5A6",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.GenreGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 60,
                    RoleId = "FE5A4BC6-A366-4AF3-8188-6F4300DAA5A6",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.PlatformTypeGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 61,
                    RoleId = "FE5A4BC6-A366-4AF3-8188-6F4300DAA5A6",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.PublisherGetPermission
                },

                //seed superadministrator claims
                new IdentityRoleClaim<string>
                {
                    Id = 62,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.OrderCreatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 63,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.OrderGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 64,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.OrderUpdatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 65,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.OrderDetailsUpdatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 66,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.OrderDetailsDeletePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 67,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.CommentCreatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 68,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.CommentDeletePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 69,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.CommentGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 70,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.GameCreatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 71,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.GameUpdatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 72,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.GameDeletePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 73,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.GameGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 74,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.GenreCreatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 75,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.GenreUpdatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 76,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.GenreDeletePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 77,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.GenreGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 78,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.PlatformTypeCreatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 79,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.PlatformTypeUpdatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 80,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.PlatformTypeDeletePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 81,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.PlatformTypeGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 82,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.PublisherCreatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 83,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.PublisherUpdatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 84,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.PublisherDeletePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 85,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.PublisherGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 86,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.RoleCreatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 87,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.RoleUpdatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 88,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.RoleDeletePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 89,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.RoleGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 90,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.RolePermissionGetPermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 91,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.UserCreatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 92,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.UserUpdatePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 93,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.UserDeletePermission
                },
                new IdentityRoleClaim<string>
                {
                    Id = 94,
                    RoleId = "CF121BD7-1C28-4B3C-9103-9BF06F61D349",
                    ClaimType = "permission",
                    ClaimValue = GlobalConstants.UserGetPermission
                });
        }
    }
}