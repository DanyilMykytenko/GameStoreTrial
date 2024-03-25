using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tests.EqualityComparers.DalEqualityComparer;

namespace GameStore.Tests.DataTests
{
    [TestFixture]
    public class GenreRepositoryTests
    {
        [TestCase(1)]
        [TestCase(2)]
        public async Task GenreRepository_GetByIdAsync_ReturnsSingleValue(int id)
        {
            using var context = new GameStoreDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var genreRepository = new GenreRepository(context);
            var genre = await genreRepository.GetByIdAsync(id);

            var expected = ExpectedGenres.FirstOrDefault(x => x.Id == id);
            Assert.That(genre, Is.EqualTo(expected).Using(new GenreEqualityComparer()));
        }
        [Test]
        public async Task GenreRepository_GetAllAsync_ReturnsAllValues()
        {
            using var context = new GameStoreDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var genreRepository = new GenreRepository(context);
            var genres = await genreRepository.GetAllAsync();

            Assert.That(genres, Is.EqualTo(ExpectedGenres).Using(new GenreEqualityComparer()));
        }
        [Test]
        public async Task GenreRepository_AddAsync_AddsValueToDatabase()
        {
            using var context = new GameStoreDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var genreRepository = new GenreRepository(context);
            var genre = new Genre { Id = 3, Name = "Rally", ParentId = 3 };

            await genreRepository.AddAsync(genre);
            await context.SaveChangesAsync();

            Assert.That(context.Genres.Count, Is.EqualTo(3));
        }
        [Test]
        public async Task GenreRepository_DeleteByIdAsync_DeletesEntity()
        {
            using var context = new GameStoreDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var genreRepository = new GenreRepository(context);

            await genreRepository.DeleteByIdAsync(1);
            await context.SaveChangesAsync();

            Assert.That(context.Genres.Count(), Is.EqualTo(1));
        }
        [Test]
        public async Task GenreRepository_Update_UpdatesEntity()
        {
            using var context = new GameStoreDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var genreRepository = new GenreRepository(context);
            var genre = new Genre
            {
                Id = 1,
                Name = "test"
            };

            genreRepository.Update(genre);
            await context.SaveChangesAsync();

            Assert.That(genre, Is.EqualTo(new Genre
            {
                Id = 1,
                Name = "test"
            }).Using(new GenreEqualityComparer()));
        }
        private static IEnumerable<Genre> ExpectedGenres =>
            new[]
            {
                new Genre { Id = 1, Name = "Action", ParentId = 2},
                new Genre { Id = 2, Name = "Shooter", ParentId = 1}
            };
    }
}
