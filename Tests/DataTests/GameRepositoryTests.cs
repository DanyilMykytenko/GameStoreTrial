using Data.Data;
using Data.Entities;
using Data.Repositories;
using static Tests.EqualityComparers.BllEqualityComparer;

namespace GameStore.Tests.DataTests
{
    [TestFixture]
    public class GameRepositoryTests
    {
        [TestCase(1)]
        [TestCase(2)]
        public async Task GameRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            using var context = new GameStoreDbContext(UnitTestHelper.GetUnitTestDbOptions());
            
            var gameRepository = new GameRepository(context);

            var game = await gameRepository.GetByIdAsync(id);

            var expected = ExpectedGames.FirstOrDefault(x => x.Id == id);
            Assert.That(game, Is.EqualTo(expected).Using(new GameEqualityComparer()), message: "GetByIdAsync method doesn't work");
        }
        [Test]
        public async Task GameRepository_GetAllAsync_ReturnsAllValues()
        {
            using var context = new GameStoreDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var gameRepository = new GameRepository(context);

            var games = await gameRepository.GetAllAsync();

            Assert.That(games, Is.EqualTo(ExpectedGames).Using(new GameEqualityComparer()), message: "GetAllAsync method doesn't work");
        }
        [Test]
        public async Task GameRepository_AddAsync_AddsValueToDatabase()
        {
            using var context = new GameStoreDbContext(UnitTestHelper.GetUnitTestDbOptions());
            
            var gameRepository = new GameRepository(context);
            var game = new Game { Id = 3, Key = "read-dead-redemption", Name = "Read Dead Redemption" };

            await gameRepository.AddAsync(game);
            await context.SaveChangesAsync();

            Assert.That(context.Games.Count, Is.EqualTo(3));
        }
        [Test]
        public async Task GameRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new GameStoreDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var gameRepository = new GameRepository(context);

            await gameRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();

            Assert.That(context.Games.Count(), Is.EqualTo(1));
        }
        [Test]
        public async Task GameRepository_Update_UpdatesEntity()
        {
            using var context = new GameStoreDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var gameRepository = new GameRepository(context);
            var game = new Game
            {
                Id = 1,
                Key = "test",
                Name = "test"
            };

            gameRepository.Update(game);
            await context.SaveChangesAsync();

            Assert.That(game, Is.EqualTo(new Game
            {
                Id = 1,
                Key = "test",
                Name = "test"
            }).Using(new GameEqualityComparer()));
        }
        private static IEnumerable<Game> ExpectedGames =>
            new[]
            {
                new Game { Id = 1, Key = "god-of-war", Name = "God Of War", Description = "none", IsDeleted = false },
                new Game { Id = 2, Key = "dark-souls", Name = "Dark Souls", Description = "none", IsDeleted = false },
            };
    }
}
