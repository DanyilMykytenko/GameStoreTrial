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
    public class PlatformServiceTests
    {
        [Test]
        public async Task PlatformService_GetAll_ReturnsAllPlatforms()
        {
            var expected = PlatformModels.ToList();
            var mockPlatformRepository = new Mock<IPlatformRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockRepository = new Mock<IRepositoryFactory>();
    
            mockPlatformRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(PlatformEntities.AsEnumerable());
    
            mockUnitOfWork.Setup(x => x.GetRepository<Platform, IPlatformRepository>())
                .Returns(mockPlatformRepository.Object);
    
            var platformService = new PlatformService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
    
            var actual = await platformService.GetAllAsync();
    
            actual.Should().BeEquivalentTo(expected, options =>
                options.Excluding(x => x.Games));
        }
        [Test]
        public async Task PlatformService_GetById_ReturnsPlatformModel()
        {
            var expected = PlatformModels.First();
            var mockPlatformRepository = new Mock<IPlatformRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockRepository = new Mock<IRepositoryFactory>();
    
            mockPlatformRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(PlatformEntities.First());
    
            mockUnitOfWork.Setup(x => x.GetRepository<Platform, IPlatformRepository>())
                .Returns(mockPlatformRepository.Object);
    
            var platformService = new PlatformService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
    
            var actual = await platformService.GetByIdAsync(1);
            actual.Should().BeEquivalentTo(expected);
        }
        [Test]
        public async Task PlatformServiceAddAsyncAddsModel()
        {
            var mockPlatformRepository = new Mock<IPlatformRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockRepository = new Mock<IRepositoryFactory>();
    
            mockPlatformRepository
                .Setup(x => x.AddAsync(It.IsAny<Platform>()));
    
            mockUnitOfWork.Setup(x => x.GetRepository<Platform, IPlatformRepository>())
                .Returns(mockPlatformRepository.Object);
    
            var platformService = new PlatformService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            var platform = CreateUpdatePlatformModels.First();
    
            await platformService.AddAsync(platform);
    
            mockPlatformRepository.Verify(x => x.AddAsync(It.Is<Platform>(x =>
                x.Id == platform.Id && x.Type == platform.Type)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }
        [TestCase(1)]
        [TestCase(2)]
        public async Task PlatformServiceDeleteAsyncDeletesGenre(int id)
        {
            var mockPlatformRepository = new Mock<IPlatformRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockRepository = new Mock<IRepositoryFactory>();
    
            mockPlatformRepository
                .Setup(x => x.DeleteByIdAsync(It.IsAny<int>()));
    
            mockUnitOfWork.Setup(x => x.GetRepository<Platform, IPlatformRepository>())
                .Returns(mockPlatformRepository.Object);
    
            var platformService = new PlatformService(mockUnitOfWork.Object, UnitTestHelper.CreateMapperProfile());
            await platformService.DeleteAsync(id);
    
            mockPlatformRepository.Verify(x => x.DeleteByIdAsync(id), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once);
        }
#region TestData
        private static List<PlatformModel> PlatformModels =>
            new List<PlatformModel>
            {
                new PlatformModel { Id = 1, Type = "PC", Games = new List<GameModel>() { } },
                new PlatformModel { Id = 2, Type = "Anroid", Games = new List<GameModel>() { } }
            }; 
        private static List<CreateUpdatePlatformModel> CreateUpdatePlatformModels =>
            new List<CreateUpdatePlatformModel>
            {
                new CreateUpdatePlatformModel { Id = 1, Type = "PC"},
                new CreateUpdatePlatformModel { Id = 2, Type = "Anroid"}
            };
        private static IEnumerable<Platform> PlatformEntities =>
            new List<Platform>
            {
                new Platform { Id = 1, Type = "PC", Games = new List<Game>() { } },
                new Platform { Id = 2, Type = "Anroid", Games = new List<Game>() { }}
            };
        #endregion
    }
}
