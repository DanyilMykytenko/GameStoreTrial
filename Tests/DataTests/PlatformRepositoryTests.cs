using Data.Data;
using Data.Entities;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tests.EqualityComparers.DalEqualityComparer.GenreEqualityComparer;

namespace GameStore.Tests.DataTests
{
    [TestFixture]
    public class PlatformRepositoryTests
    {
        [TestCase(1)]
        [TestCase(2)]
        public async Task PlatformRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            using var context = new GameStoreDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var platformRepository = new PlatformRepository(context);
            var platform = await platformRepository.GetByIdAsync(id);

            var expected = ExpectedPlatforms.FirstOrDefault(x => x.Id == id);
            Assert.That(platform, Is.EqualTo(expected).Using(new PlatformEqualityComparer()));
        }
        [Test]
        public async Task PlatformRepository_GetAllAsync_ReturnsAllValues()
        {
            using var context = new GameStoreDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var platformRepository = new PlatformRepository(context);
            var platform = await platformRepository.GetAllAsync();

            Assert.That(platform, Is.EqualTo(ExpectedPlatforms).Using(new PlatformEqualityComparer()));
        }
        [Test]
        public async Task PlatformRepository_AddAsync_AddsValueToDatabase()
        {
            using var context = new GameStoreDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var platformRepository = new PlatformRepository(context);
            var platform = new Platform { Id = 3, Type = "PS" };

            await platformRepository.AddAsync(platform);
            await context.SaveChangesAsync();

            Assert.That(context.Platforms.Count, Is.EqualTo(3));
        }
        [Test]
        public async Task PlatformRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new GameStoreDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var platformRepository = new PlatformRepository(context);

            await platformRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();

            Assert.That(context.Platforms.Count(), Is.EqualTo(1));
        }
        [Test]
        public async Task PlatformRepository_Update_UpdatesEntity()
        {
            using var context = new GameStoreDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var platformRepository = new PlatformRepository(context);
            var platform = new Platform
            {
                Id = 1,
                Type = "test"
            };

            platformRepository.Update(platform);
            await context.SaveChangesAsync();

            Assert.That(platform, Is.EqualTo(new Platform
            {
                Id = 1,
                Type = "test"
            }).Using(new PlatformEqualityComparer()));
        }
        private static IEnumerable<Platform> ExpectedPlatforms =>
            new[]
            {
                new Platform { Id = 1, Type = "PC" },
                new Platform { Id = 2, Type = "Anroid" }
            };
    }
}
