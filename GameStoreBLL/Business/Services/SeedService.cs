using Business.Interfaces;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using MongoData.Entities;
using MongoData.Enums;
using MongoDB.Driver;

namespace Business.Services
{
    public class SeedService : ISeedService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMongoCollection<Product> _products;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public SeedService(IUnitOfWork unitOfWork, IMongoDatabase database, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _unitOfWork = unitOfWork;
            _products = database.GetCollection<Product>(CollectionNameEnums.Products.ToString());
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedData()
        {
            await _unitOfWork.GetRepository<Basket, IBasketRepository>().AddAsync(new Basket
            {
                Details = null,
                CustomerId = null
            });
            await _unitOfWork.GetRepository<Genre, IGenreRepository>().AddAsync(
                new Genre { Name = "Rally" });
            await _unitOfWork.GetRepository<Genre, IGenreRepository>().AddAsync(
                new Genre { Name = "RogueLike" });

            await _unitOfWork.GetRepository<Platform, IPlatformRepository>().AddAsync(
                new Platform { Type = "PS4" });
            await _unitOfWork.GetRepository<Platform, IPlatformRepository>().AddAsync(
                new Platform { Type = "IOS" });

            await _unitOfWork.GetRepository<Publisher, IPublisherRepository>().AddAsync(
                new Publisher { CompanyName = "Sunrise" });
            await _unitOfWork.GetRepository<Publisher, IPublisherRepository>().AddAsync(
                new Publisher { CompanyName = "Valve" });

            var genreToSeed = await _unitOfWork.GetRepository<Genre, IGenreRepository>().GetAllAsync(x => x.Name == "Rally");
            var platformToSeed = await _unitOfWork.GetRepository<Platform, IPlatformRepository>().GetAllAsync(x => x.Type == "IOS");

            await _unitOfWork.GetRepository<Game, IGameRepository>().AddAsync(
                new Game
                {
                    Name = "Sekiro",
                    Key = "sekiro",
                    Description = "Seeding",
                    Genres = genreToSeed,
                    Platforms = platformToSeed,
                    Viewed = 1
                });
            await _unitOfWork.GetRepository<Game, IGameRepository>().AddAsync(
                new Game
                {
                    Name = "Call Of Duty",
                    Key = "call-of-duty",
                    Description = "Seeding",
                    Genres = genreToSeed,
                    Platforms = platformToSeed,
                    Viewed = 1
                });

            await _unitOfWork.SaveAsync();
            await SeedMongo();
            await SeedSuperAdmin();
        }
        public async Task SeedMongo()
        {
            var documents = await _products.Find(Builders<Product>.Filter.Empty).ToListAsync();

            foreach (var document in documents)
            {
                var filter = Builders<Product>.Filter.Eq("_id", document.Id);
                var update = Builders<Product>.Update
                    .Set("Key", document.ProductName.ToLowerInvariant().Replace(' ', '-'))
                    .Set("Viewed", 0);

                await _products.UpdateOneAsync(filter, update);
            }
        }
        public async Task SeedSuperAdmin()
        {
            var superAdmin = new User
            {
                FirstName = "admin",
                LastName = "super",
                UserName = "admin",
                Email = "superadmin@example.com"
            };
            var result = await _userManager.CreateAsync(superAdmin, "1234");

            var admin = await _userManager.FindByNameAsync("admin");

            await _userManager.AddToRoleAsync(admin, "SuperAdministrator");
        }
    }
}
