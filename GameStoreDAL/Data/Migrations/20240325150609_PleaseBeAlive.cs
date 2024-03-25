using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class PleaseBeAlive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Basket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basket", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FromMongo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Genres_Genres_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Genres",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isPaid = table.Column<bool>(type: "bit", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Platforms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platforms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomePage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromMongo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<short>(type: "smallint", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    BasketId = table.Column<int>(type: "int", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Basket_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Basket",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitInStock = table.Column<short>(type: "smallint", nullable: false),
                    Discontinued = table.Column<int>(type: "int", nullable: false),
                    PublisherId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Viewed = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    FromMongo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    GameId = table.Column<int>(type: "int", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Comments_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Comments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GameGenre",
                columns: table => new
                {
                    GamesId = table.Column<int>(type: "int", nullable: false),
                    GenresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameGenre", x => new { x.GamesId, x.GenresId });
                    table.ForeignKey(
                        name: "FK_GameGenre_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameGenre_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamePlatform",
                columns: table => new
                {
                    GamesId = table.Column<int>(type: "int", nullable: false),
                    PlatformsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlatform", x => new { x.GamesId, x.PlatformsId });
                    table.ForeignKey(
                        name: "FK_GamePlatform_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePlatform_Platforms_PlatformsId",
                        column: x => x.PlatformsId,
                        principalTable: "Platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4CEDE834-14E8-4392-8DC7-ABA9E4DD4B71", "0433f32d-5d8d-4ce9-82ae-b35a6b55a97f", "User", "USER" },
                    { "54DCC2BB-0145-441B-B148-5EB18A9BC34B", "e498b4e9-a4c1-4e47-b5ba-bbb4fe795b4f", "Guest", "GUEST" },
                    { "C2972C01-B621-4D34-9A53-0072FFBC8784", "781f98c5-ac60-4170-97e8-df1ce1e94af3", "Manager", "MANAGER" },
                    { "CD712C58-B6AF-4881-92BD-4D1B2F7A171B", "0c2741c1-eb83-46e7-bdb5-bfad8ea6c620", "Administrator", "ADMINISTRATOR" },
                    { "CF121BD7-1C28-4B3C-9103-9BF06F61D349", "032ab1a9-e71d-4e6d-80a9-9770d86abb45", "SuperAdministrator", "SUPERADMINISTRATOR" },
                    { "F3C74A98-0709-4467-9A14-7EB18950725D", "b2d2f6db-e743-42a4-8797-a247ba2356ee", "Moderator", "MODERATOR" },
                    { "FE5A4BC6-A366-4AF3-8188-6F4300DAA5A6", "93de26b0-f108-4558-b8fc-129d90b3d4d6", "Publisher", "PUBLISHER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "permission", "role.create", "CD712C58-B6AF-4881-92BD-4D1B2F7A171B" },
                    { 2, "permission", "role.update", "CD712C58-B6AF-4881-92BD-4D1B2F7A171B" },
                    { 3, "permission", "role.delete", "CD712C58-B6AF-4881-92BD-4D1B2F7A171B" },
                    { 4, "permission", "role.get", "CD712C58-B6AF-4881-92BD-4D1B2F7A171B" },
                    { 5, "permission", "role.permission.get", "CD712C58-B6AF-4881-92BD-4D1B2F7A171B" },
                    { 6, "permission", "user.create", "CD712C58-B6AF-4881-92BD-4D1B2F7A171B" },
                    { 7, "permission", "user.update", "CD712C58-B6AF-4881-92BD-4D1B2F7A171B" },
                    { 8, "permission", "user.delete", "CD712C58-B6AF-4881-92BD-4D1B2F7A171B" },
                    { 9, "permission", "user.get", "CD712C58-B6AF-4881-92BD-4D1B2F7A171B" },
                    { 10, "permission", "comment.create", "CD712C58-B6AF-4881-92BD-4D1B2F7A171B" },
                    { 11, "permission", "comment.delete", "CD712C58-B6AF-4881-92BD-4D1B2F7A171B" },
                    { 12, "permission", "comment.get", "CD712C58-B6AF-4881-92BD-4D1B2F7A171B" },
                    { 13, "permission", "game.create", "C2972C01-B621-4D34-9A53-0072FFBC8784" },
                    { 14, "permission", "game.update", "C2972C01-B621-4D34-9A53-0072FFBC8784" },
                    { 15, "permission", "game.delete", "C2972C01-B621-4D34-9A53-0072FFBC8784" },
                    { 16, "permission", "game.get", "C2972C01-B621-4D34-9A53-0072FFBC8784" },
                    { 17, "permission", "genre.create", "C2972C01-B621-4D34-9A53-0072FFBC8784" },
                    { 18, "permission", "genre.update", "C2972C01-B621-4D34-9A53-0072FFBC8784" },
                    { 19, "permission", "genre.delete", "C2972C01-B621-4D34-9A53-0072FFBC8784" },
                    { 20, "permission", "genre.get", "C2972C01-B621-4D34-9A53-0072FFBC8784" },
                    { 21, "permission", "platformtype.create", "C2972C01-B621-4D34-9A53-0072FFBC8784" },
                    { 22, "permission", "platformtype.update", "C2972C01-B621-4D34-9A53-0072FFBC8784" },
                    { 23, "permission", "platformtype.delete", "C2972C01-B621-4D34-9A53-0072FFBC8784" },
                    { 24, "permission", "platformtype.get", "C2972C01-B621-4D34-9A53-0072FFBC8784" },
                    { 25, "permission", "publisher.create", "C2972C01-B621-4D34-9A53-0072FFBC8784" },
                    { 26, "permission", "publisher.update", "C2972C01-B621-4D34-9A53-0072FFBC8784" },
                    { 27, "permission", "publisher.delete", "C2972C01-B621-4D34-9A53-0072FFBC8784" },
                    { 28, "permission", "publisher.get", "C2972C01-B621-4D34-9A53-0072FFBC8784" },
                    { 29, "permission", "order.create", "C2972C01-B621-4D34-9A53-0072FFBC8784" },
                    { 30, "permission", "order.get", "C2972C01-B621-4D34-9A53-0072FFBC8784" },
                    { 31, "permission", "order.update", "C2972C01-B621-4D34-9A53-0072FFBC8784" },
                    { 32, "permission", "order.details.update", "C2972C01-B621-4D34-9A53-0072FFBC8784" },
                    { 33, "permission", "order.details.delete", "C2972C01-B621-4D34-9A53-0072FFBC8784" },
                    { 34, "permission", "comment.create", "F3C74A98-0709-4467-9A14-7EB18950725D" },
                    { 35, "permission", "comment.delete", "F3C74A98-0709-4467-9A14-7EB18950725D" },
                    { 36, "permission", "comment.get", "F3C74A98-0709-4467-9A14-7EB18950725D" },
                    { 37, "permission", "order.get", "F3C74A98-0709-4467-9A14-7EB18950725D" },
                    { 38, "permission", "game.get", "F3C74A98-0709-4467-9A14-7EB18950725D" },
                    { 39, "permission", "genre.get", "F3C74A98-0709-4467-9A14-7EB18950725D" },
                    { 40, "permission", "platformtype.get", "F3C74A98-0709-4467-9A14-7EB18950725D" },
                    { 41, "permission", "publisher.get", "F3C74A98-0709-4467-9A14-7EB18950725D" },
                    { 42, "permission", "comment.get", "4CEDE834-14E8-4392-8DC7-ABA9E4DD4B71" },
                    { 43, "permission", "order.get", "4CEDE834-14E8-4392-8DC7-ABA9E4DD4B71" },
                    { 44, "permission", "game.get", "4CEDE834-14E8-4392-8DC7-ABA9E4DD4B71" },
                    { 45, "permission", "genre.get", "4CEDE834-14E8-4392-8DC7-ABA9E4DD4B71" },
                    { 46, "permission", "platformtype.get", "4CEDE834-14E8-4392-8DC7-ABA9E4DD4B71" },
                    { 47, "permission", "publisher.get", "4CEDE834-14E8-4392-8DC7-ABA9E4DD4B71" },
                    { 48, "permission", "comment.create", "4CEDE834-14E8-4392-8DC7-ABA9E4DD4B71" },
                    { 49, "permission", "comment.create", "54DCC2BB-0145-441B-B148-5EB18A9BC34B" },
                    { 50, "permission", "comment.get", "54DCC2BB-0145-441B-B148-5EB18A9BC34B" },
                    { 51, "permission", "game.get", "54DCC2BB-0145-441B-B148-5EB18A9BC34B" },
                    { 52, "permission", "genre.get", "54DCC2BB-0145-441B-B148-5EB18A9BC34B" },
                    { 53, "permission", "platformtype.get", "54DCC2BB-0145-441B-B148-5EB18A9BC34B" },
                    { 54, "permission", "publisher.get", "54DCC2BB-0145-441B-B148-5EB18A9BC34B" },
                    { 55, "permission", "comment.create", "FE5A4BC6-A366-4AF3-8188-6F4300DAA5A6" },
                    { 56, "permission", "comment.get", "FE5A4BC6-A366-4AF3-8188-6F4300DAA5A6" },
                    { 57, "permission", "order.get", "FE5A4BC6-A366-4AF3-8188-6F4300DAA5A6" },
                    { 58, "permission", "game.get", "FE5A4BC6-A366-4AF3-8188-6F4300DAA5A6" },
                    { 59, "permission", "genre.get", "FE5A4BC6-A366-4AF3-8188-6F4300DAA5A6" },
                    { 60, "permission", "platformtype.get", "FE5A4BC6-A366-4AF3-8188-6F4300DAA5A6" },
                    { 61, "permission", "publisher.get", "FE5A4BC6-A366-4AF3-8188-6F4300DAA5A6" },
                    { 62, "permission", "order.create", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 63, "permission", "order.get", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 64, "permission", "order.update", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 65, "permission", "order.details.update", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 66, "permission", "order.details.delete", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 67, "permission", "comment.create", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 68, "permission", "comment.delete", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 69, "permission", "comment.get", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 70, "permission", "game.create", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 71, "permission", "game.update", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 72, "permission", "game.delete", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 73, "permission", "game.get", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 74, "permission", "genre.create", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 75, "permission", "genre.update", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 76, "permission", "genre.delete", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 77, "permission", "genre.get", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 78, "permission", "platformtype.create", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 79, "permission", "platformtype.update", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 80, "permission", "platformtype.delete", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 81, "permission", "platformtype.get", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 82, "permission", "publisher.create", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 83, "permission", "publisher.update", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 84, "permission", "publisher.delete", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 85, "permission", "publisher.get", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 86, "permission", "role.create", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 87, "permission", "role.update", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 88, "permission", "role.delete", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 89, "permission", "role.get", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 90, "permission", "role.permission.get", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 91, "permission", "user.create", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 92, "permission", "user.update", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 93, "permission", "user.delete", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" },
                    { 94, "permission", "user.get", "CF121BD7-1C28-4B3C-9103-9BF06F61D349" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_GameId",
                table: "Comments",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentId",
                table: "Comments",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_GameGenre_GenresId",
                table: "GameGenre",
                column: "GenresId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlatform_PlatformsId",
                table: "GamePlatform",
                column: "PlatformsId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Key",
                table: "Games",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_PublisherId",
                table: "Games",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_Name",
                table: "Genres",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genres_ParentId",
                table: "Genres",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_BasketId",
                table: "OrderDetails",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_CompanyName",
                table: "Publishers",
                column: "CompanyName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "GameGenre");

            migrationBuilder.DropTable(
                name: "GamePlatform");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Platforms");

            migrationBuilder.DropTable(
                name: "Basket");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
