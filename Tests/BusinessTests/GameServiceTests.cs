using Business.Models;
using Business.Services;
using Data.Entities;
using Data.Interfaces;
using FluentAssertions;
using Moq;

namespace GameStore.Tests.BusinessTests
{
    [TestFixture]
    public class GameServiceTests
    {
        // [Test]
        // public async Task GameServiceGetAllReturnsAllGames()
        // {
        //     var expected = GetTestGameModels;
        //     var mockUnitOfWork = new Mock<IUnitOfWork>();
        //
        //     var mockRepository = new Mock<IRepositoryFactory>();
        //     var mockGameRepository = new Mock<IGameRepository>();
        //
        //     mockGameRepository
        //         .Setup(x => x.GetAllAsync())
        //         .ReturnsAsync(GetTestGameEntites.AsEnumerable());
        //
        //     mockUnitOfWork.Setup(x => x.GetRepository<Game, IGameRepository>())
        //         .Returns(mockGameRepository.Object);
        //
        //     var gameService = new GameService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
        //
        //     var actual = await gameService.GetAllAsync();
        //
        //     actual.Should().BeEquivalentTo(expected);
        // }
        // [Test]
        // public async Task GameServiceGetByIdReturnsGameModel()
        // {
        //     var expected = GetTestGameModels.First();
        //
        //     var mockUnitOfWork = new Mock<IUnitOfWork>();
        //     var mockGameRepository = new Mock<IGameRepository>();
        //     var mockRepository = new Mock<IRepositoryFactory>();
        //
        //     mockGameRepository
        //         .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
        //         .ReturnsAsync(GetTestGameEntites.First());
        //
        //     mockUnitOfWork.Setup(x => x.GetRepository<Game, IGameRepository>())
        //         .Returns(mockGameRepository.Object);
        //
        //     var gameService = new GameService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
        //
        //     var actual = await gameService.GetByIdAsync(1);
        //     actual.Should().BeEquivalentTo(expected);
        // }
        // [TestCase(1)]
        // [TestCase(2)]
        // public async Task GameServiceDeleteAsyncDeletesGame(int id)
        // {
        //     var mockGameRepository = new Mock<IGameRepository>();
        //     var mockUnitOfWork = new Mock<IUnitOfWork>();
        //     var mockRepository = new Mock<IRepositoryFactory>();
        // 
        //     mockUnitOfWork.Setup(x => x.GetRepository<Game, IGameRepository>())
        //         .Returns(mockGameRepository.Object);
        // 
        //     mockGameRepository
        //         .Setup(x => x.DeleteByIdAsync(It.IsAny<int>()));
        // 
        // 
        //     var gameService = new GameService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
        // 
        //     await gameService.DeleteAsync(id);
        // 
        //     mockGameRepository.Verify(x => x.DeleteByIdAsync(id), Times.Once);
        //     mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        // }

        #region TestData
        private static List<GameModel> GetTestGameModels =>
            new List<GameModel>
            {
                new GameModel { Id = 1, Key = "god-of-war", Name = "God of War", Description = "none", Genres = new List<GenreModel>() { }, Platforms = new List<PlatformModel>() { }},
                new GameModel { Id = 2, Key = "dark-souls", Name = "Dark Souls", Description = "none", Genres = new List<GenreModel>() { }, Platforms = new List<PlatformModel>() { }}
            };
        private static List<Game> GetTestGameEntites =>
            new List<Game>
            {
                new Game { Id = 1, Key = "god-of-war", Name = "God of War", Description = "none", Genres = new List<Genre>() { }, Platforms = new List<Platform>() { } },
                new Game { Id = 2, Key = "dark-souls", Name = "Dark Souls", Description = "none", Genres = new List<Genre>() { }, Platforms = new List<Platform>() { } }
            };

        private static List<CreateUpdateGameModel> GetTestCreateUpdateGameModels =>
            new List<CreateUpdateGameModel>
            {
                new CreateUpdateGameModel { Id = 1, Key = "god-of-war", Name = "God of War", Description = "none", Price = 1, Discontinued = 1, UnitInStock = 1},
                new CreateUpdateGameModel { Id = 2, Key = "dark-souls", Name = "Dark Souls", Description = "none", Price = 1, Discontinued = 1, UnitInStock = 1}
            }; 

        private static List<GenreModel> GenreModels =>
            new List<GenreModel>
            {
                new GenreModel { Id = 1, Name = "Action", ParentId = 2, Games = new List<GameModel>() { }, Nested = new List<GenreModel>() { }},
                new GenreModel { Id = 2, Name = "Shooter", ParentId = 1, Games = new List<GameModel>() { }, Nested = new List<GenreModel>() { }}
            };
        private static List<Genre> GenreEntities =>
            new List<Genre>
            {
                new Genre { Id = 1, Name = "Action", ParentId = 2, Games = new List<Game>() { }, Nested = new List<Genre>() { }},
                new Genre { Id = 2, Name = "Shooter", ParentId = 1, Games = new List<Game>() { }, Nested = new List<Genre>() { }}
            };
        private static List<PlatformModel> PlatformModels =>
            new List<PlatformModel>
            {
                new PlatformModel { Id = 1, Type = "PC", Games = new List<GameModel>() { }},
                new PlatformModel { Id = 2, Type = "Anroid", Games = new List<GameModel>() { }}
            };
        private static List<Platform> PlatformEntities =>
            new List<Platform>
            {
                new Platform { Id = 1, Type = "PC", Games = new List<Game>() { } },
                new Platform { Id = 2, Type = "Anroid", Games = new List<Game>() { }}
            };
        #endregion
    }
}
