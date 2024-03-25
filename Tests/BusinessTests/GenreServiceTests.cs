using Business.Models;
using Business.Services;
using Data.Data;
using Data.Entities;
using Data.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace GameStore.Tests.BusinessTests
{
    [TestFixture]
    public class GenreServiceTests
    {
        [Test]
        public async Task GenreService_GetAll_ReturnsAllGenres()
        {
            var expected = GenreModels.ToList();
            var mockGenreRepository = new Mock<IGenreRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockRepository = new Mock<IRepositoryFactory>();
    
            mockGenreRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(GenreEntities.AsEnumerable());
    
            mockUnitOfWork.Setup(x => x.GetRepository<Genre, IGenreRepository>())
                .Returns(mockGenreRepository.Object);
    
            var genreService = new GenreService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
    
            var actual = await genreService.GetAllAsync();
    
            actual.Should().BeEquivalentTo(expected, options =>
                options.Excluding(x => x.Games));
        }
        [Test]
        public async Task GenreService_GetById_ReturnsGenreModel()
        {
            var expected = GenreModels.First();
            var mockGenreRepository = new Mock<IGenreRepository>(); 
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockRepository = new Mock<IRepositoryFactory>();
    
            mockGenreRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(GenreEntities.First());
    
            mockUnitOfWork.Setup(x => x.GetRepository<Genre, IGenreRepository>())
                .Returns(mockGenreRepository.Object);
    
            var genreService = new GenreService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            
            var actual = await genreService.GetByIdAsync(1);
            actual.Should().BeEquivalentTo(expected);
        }
        [Test]
        public async Task GenreServiceAddAsyncAddsModel()
        {
            var mockGenreRepository = new Mock<IGenreRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockRepository = new Mock<IRepositoryFactory>();
            var genre = CreateUpdateGenreModels.First();
    
            mockGenreRepository.Setup(x => x.AddAsync(It.Is<Genre>(x => x.Id == genre.Id && x.Name == genre.Name)));
    
            mockUnitOfWork.Setup(x => x.GetRepository<Genre, IGenreRepository>())
                .Returns(mockGenreRepository.Object);
    
            var genreService = new GenreService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
    
            await genreService.AddAsync(genre);
    
            mockGenreRepository.Verify(x => x.AddAsync(It.Is<Genre>(x =>
                x.Id == genre.Id && x.Name == genre.Name)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }
        [TestCase(1)]
        [TestCase(2)]
        public async Task GenreServiceDeleteAsyncDeletesGenre(int id)
        {
            var mockGenreRepository = new Mock<IGenreRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockRepository = new Mock<IRepositoryFactory>();
    
            mockGenreRepository
                .Setup(x => x.DeleteByIdAsync(It.IsAny<int>()));
    
            mockUnitOfWork.Setup(x => x.GetRepository<Genre, IGenreRepository>())
                .Returns(mockGenreRepository.Object);
    
            var genreService = new GenreService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            await genreService.DeleteAsync(id);
    
            mockGenreRepository.Verify(x => x.DeleteByIdAsync(id), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }
    
        #region TestData
        private static List<GenreModel> GenreModels =>
            new List<GenreModel>
            {
                new GenreModel { Id = 1, Name = "Action", Games = new List<GameModel>() { }, Nested = new List<GenreModel>() { } },
                new GenreModel { Id = 2, Name = "Shooter", ParentId = 1, Games = new List<GameModel>() { }, Nested = new List<GenreModel>() { } }
            }; 
        private static List<CreateUpdateGenreModel> CreateUpdateGenreModels =>
            new List<CreateUpdateGenreModel>
            {
                new CreateUpdateGenreModel { Id = 1, Name = "Action"},
                new CreateUpdateGenreModel { Id = 2, Name = "Shooter"}
            };
        private static List<Genre> GenreEntities =>
            new List<Genre>
            {
                new Genre { Id = 1, Name = "Action", Games = new List<Game>() { }, Nested = new List<Genre>() { } },
                new Genre { Id = 2, Name = "Shooter", ParentId = 1, Games = new List<Game>() { }, Nested = new List<Genre>() { } }
            };
        #endregion
    }
}
