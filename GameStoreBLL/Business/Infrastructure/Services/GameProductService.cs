using AutoMapper;
using Business.Builders;
using Business.Enums;
using Business.Infrastructure.Interfaces;
using Business.Interfaces;
using Business.Models;
using Business.Services;
using Business.Validation;
using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Data.Models;
using MongoData.Entities;
using MongoData.Interfaces;
using MongoDB.Driver;

namespace Business.Infrastructure.Services
{
    public class GameProductService : IGameProductService
    {
        private readonly ISuppliersRepository _suppliersRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBasketService _basketService;
        private readonly ICommentService _commentService;
        private readonly IGenreService _genreService;
        private readonly IPublisherService _publisherService;
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceProvider _serviceProvider;
        private readonly GameStoreDbContext _context;
        private readonly QueryDirector _queryDirector;
        private readonly MongoDirector _mongoDirector;
        private readonly BusinessDirector _businessDirector;
        private const string NullableAllCount = "0";

        public GameProductService(IProductRepository productRepository, IMapper mapper,
            IUnitOfWork unitOfWork, ISuppliersRepository suppliersRepository,
            ICategoryRepository categoryRepository, IGameService gameService,
            IBasketService basketService, ICommentService commentService,
            IGenreService genreService, IPublisherService publisherService, GameStoreDbContext context, IServiceProvider serviceProvider,
            QueryDirector queryDirector, MongoDirector mongoDirector, BusinessDirector businessDirector)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _suppliersRepository = suppliersRepository;
            _categoryRepository = categoryRepository;
            _gameService = gameService;
            _basketService = basketService;
            _commentService = commentService;
            _genreService = genreService;
            _publisherService = publisherService;
            _context = context;
            _serviceProvider = serviceProvider;
            _queryDirector = queryDirector;
            _mongoDirector = mongoDirector;
            _businessDirector = businessDirector;
        }

        public async Task AddAsync(CreateUpdateGameModel model)
        {
            if (model is null)
            {
                throw new GameStoreException("Model is null.");
            }

            if (await _unitOfWork.GetRepository<Game, IGameRepository>().GetByKeyAsync(model.Key) is not null)
            {
                throw new GameStoreException("Duplicate key!");
            }

            if (string.IsNullOrEmpty(model.Key))
            {
                model.Key = GenerateGameKey(model);
            }

            var game = _mapper.Map<Game>(model);

            await CheckRelatedGenre(model.Genres, game);

            if (model.Publisher != null)
            {
                await CheckRelatedPublisher(model.Publisher.Value, game);
            }

            await SetPlatformsAsync(model.Platforms, game);

            game.CreationDate = DateTime.UtcNow;

            await _unitOfWork.GetRepository<Game, IGameRepository>().AddAsync(game);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var game = await GetByIdAsync(id);
            var database = InfrastructureHelper.DefineDatabase(id, ConstService.GameId);
            switch (database)
            {
                case DatabaseEnum.SQL:
                    await _unitOfWork.GetRepository<Game, IGameRepository>().DeleteByIdAsync(game.Id);
                    break;
                case DatabaseEnum.Mongo:
                    var gameFromProduct = new Game
                    {
                        Name = game.Name,
                        Key = game.Key,
                        Description = game.Description,
                        Discontinued = game.Discontinued,
                        Price = game.Price,
                        UnitInStock = game.UnitInStock,
                        CreationDate = game.CreationDate,
                        FromMongo = game.Name,
                        IsDeleted = true
                    };
                    await _unitOfWork.GetRepository<Game, IGameRepository>().AddAsync(gameFromProduct);
                    break;
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task<GetGamesResponseModel> GetAllAsync(GetGamesRequestModel model)
        {
            var filter = _mapper.Map<Filter>(model);

            var games = await SortAsync(filter);

            if (filter.PageCount == PaggingEnum.All.ToString())
            {
                filter.PageCount = games.Count().ToString();
            }

            var result = new GetGamesResponseModel
            {
                Games = _mapper.Map<ICollection<GameModel>>(games),
                CurrentPage = model.Page.Value,
                TotalPages = filter.PageCount == NullableAllCount
                    ? NullableAllCount
                    : (games.Count() / int.Parse(filter.PageCount)).ToString()
            };
            return _mapper.Map<GetGamesResponseModel>(result);
        }

        public async Task<IEnumerable<GameModel>> SortAsync(Filter filter)
        {
            var sort = _mapper.Map<Filter>(filter);

            var sqlExpression = _queryDirector.Sort(sort);
            var games = await _unitOfWork.GetRepository<Game, IGameRepository>().GetAllAsync(sqlExpression);
            var deletedGames = await _unitOfWork.GetRepository<Game, IGameRepository>().GetAllAsync(x => x.IsDeleted == true);

            //var mongoExpression = _mongoDirector.Sort(sort);
            //var products = await _productRepository.GetAllAsync(mongoExpression);

            //var mappedProducts = _mapper.Map<IEnumerable<Game>>(products);
            //var result = mappedProducts.Where(x => !games.Any(y => y.FromMongo == x.Name) && !deletedGames.Any(d => d.FromMongo == x.Name));
            //var concated = games.Concat(result) as IEnumerable<Game>;

            //concated = _businessDirector.Sort(filter, concated);
            return _mapper.Map<IEnumerable<GameModel>>(games);
        }

        public async Task<GameModel> GetByIdAsync(int id)
        {
            var database = InfrastructureHelper.DefineDatabase(id, ConstService.GameId);
            switch (database)
            {
                case DatabaseEnum.SQL:
                    var game = await _unitOfWork.GetRepository<Game, IGameRepository>().GetByIdAsync(id);
                    return _mapper.Map<GameModel>(game);
                case DatabaseEnum.Mongo:
                    var product = _mapper.Map<Game>(await _productRepository.GetByIdAsync(id));
                    return _mapper.Map<GameModel>(product);
                default:
                    throw new NotFoundException(nameof(id));
            }
        }

        public async Task<GameModel> GetByIdWithDetails(int id)
        {
            var database = InfrastructureHelper.DefineDatabase(id, ConstService.GameId);
            switch (database)
            {
                case DatabaseEnum.SQL:
                    var game = await _unitOfWork.GetRepository<Game, IGameRepository>().GetByIdWithDetails(id);
                    return _mapper.Map<GameModel>(game);
                case DatabaseEnum.Mongo:
                    return _mapper.Map<GameModel>(await _productRepository.GetByIdAsync(id));
                default:
                    throw new NotFoundException(nameof(id));
            }
        }

        public async Task<GameModel> GetByKeyAsync(string key)
        {
            var game = await _unitOfWork.GetRepository<Game, IGameRepository>().GetByKeyAsync(key);
            if (game is null)
            {
                var product = await _productRepository.GetByKeyAsync(key);
                var genre = _mapper.Map<Genre>(await _categoryRepository.GetByIdAsync(product.CategoryID));
                var publisher = _mapper.Map<Publisher>(await _suppliersRepository.GetByIdAsync(product.SupplierID));
                game = _mapper.Map<Game>(product);
                game.Genres = new List<Genre> { genre };
                game.Publisher = publisher;
            }
            return _mapper.Map<GameModel>(game);
        }

        public async Task<IEnumerable<GameModel>> GetByGenreAsync(int id)
        {
            var games = await _unitOfWork.GetRepository<Game, IGameRepository>().GetByGenreAsync(id);
            if (games is null)
            {
                games = _mapper.Map<IEnumerable<Game>>(await _productRepository.GetByGenreAsync(id));
            }
            return _mapper.Map<IEnumerable<GameModel>>(games);
        }

        public async Task<IEnumerable<GameModel>> GetByPlatformAsync(int id)
        {
            return await _gameService.GetByPlatformAsync(id);
        }

        public async Task<IEnumerable<GameModel>> GetByPublisherAsync(string companyName)
        {
            var games = await _unitOfWork.GetRepository<Game, IGameRepository>().GetByPublisherAsync(companyName);
            if (games is null)
            {
                games = _mapper.Map<IEnumerable<Game>>(await _productRepository.GetByPublisherAsync(companyName));
            }
            return _mapper.Map<IEnumerable<GameModel>>(games);
        }

        public async Task<GameModel> GetGameById(int id)
        {
            var game = await _unitOfWork.GetRepository<Game, IGameRepository>().GetByIdAsync(id);
            if (game is null)
            {
                game = _mapper.Map<Game>(await _productRepository.GetByIdAsync(id));
            }

            return _mapper.Map<GameModel>(game);
        }

        public async Task Update(CreateUpdateGameModel entity)
        {
            var database = InfrastructureHelper.DefineDatabase(entity.Id.Value, ConstService.GameId);

            if (database == DatabaseEnum.SQL)
            {
                await _unitOfWork.SaveAsync();
            }
        }


        private async Task<Genre> AddRelatedGenre(Category category)
        {
            var checkGenre = await _unitOfWork.GetRepository<Genre, IGenreRepository>().GetAllAsync(x => x.Name == category.CategoryName);
            if (checkGenre.Count == 0)
            {
                var genreToAdd = new Genre
                {
                    ParentId = null,
                    Name = category.CategoryName,
                    FromMongo = category.CategoryName,
                    Games = null,
                    Nested = null,
                    Parent = null
                };
                await _unitOfWork.GetRepository<Genre, IGenreRepository>().AddAsync(genreToAdd);
                await _unitOfWork.SaveAsync();
                return genreToAdd;
            }
            return checkGenre.First();

        }
        private async Task<Publisher> AddRelatedPublisher(Supplier supplier)
        {
            var checkPublisher = await _unitOfWork.GetRepository<Publisher, IPublisherRepository>().GetByCompanyNameAsync(supplier.CompanyName);
            if (checkPublisher is null)
            {
                var publisherToAdd = new Publisher
                {
                    CompanyName = supplier.CompanyName,
                    Description = supplier.Address,
                    HomePage = supplier.HomePage,
                    FromMongo = supplier.CompanyName,
                    Games = null
                };
                await _unitOfWork.GetRepository<Publisher, IPublisherRepository>().AddAsync(publisherToAdd);
                await _unitOfWork.SaveAsync();
                var newPublisher = await _unitOfWork.GetRepository<Publisher, IPublisherRepository>().GetByCompanyNameAsync(publisherToAdd.CompanyName);
                return newPublisher;
            }
            return checkPublisher;
        }
        private async Task CheckRelatedGenre(List<int> ids, Game game)
        {
            ICollection<Genre> genres = new List<Genre>();
            foreach (int id in ids)
            {
                var genre = await _unitOfWork.GetRepository<Genre, IGenreRepository>().GetByIdAsync(id);
                if (genre is null)
                {
                    var category = await _categoryRepository.GetByIdAsync(id);
                    var genreToAdd = new Genre
                    {
                        ParentId = null,
                        Name = category.CategoryName,
                        FromMongo = category.CategoryName,
                        Games = null,
                        Nested = null,
                        Parent = null
                    };
                    await _unitOfWork.GetRepository<Genre, IGenreRepository>().AddAsync(genreToAdd);
                    await _unitOfWork.SaveAsync();
                    var newGenre = await _unitOfWork.GetRepository<Genre, IGenreRepository>().GetAllAsync(x => x.Name == genreToAdd.Name);
                    genres.Add(newGenre.First());
                }
                else
                {
                    genres.Add(genre);
                }
            }

            var allGenres = await _unitOfWork.GetRepository<Genre, IGenreRepository>().GetAllAsync();

            var genresToAdd = allGenres.Where(x => genres.Any(y => x.Name == y.Name));

            var genresToDelete = allGenres.Where(x => !genres.Any(y => x.Name == y.Name));

            foreach (var genreToAdd in genresToAdd)
            {
                game.Genres.Add(genreToAdd);
            }

            foreach (var genreToRemove in genresToDelete)
            {
                game.Genres.Remove(genreToRemove);
            }
        }
        private async Task SetPlatformsAsync(List<int> Platforms, Game game)
        {
            var allPlatforms = await _unitOfWork.GetRepository<Platform, IPlatformRepository>().GetAllAsync();

            var platformsToAdd = allPlatforms.Where(x => Platforms.Any(y => x.Id == y));

            var platformsToDelete = allPlatforms.Where(x => !Platforms.Any(y => x.Id == y));

            foreach (var platformToAdd in platformsToAdd)
            {
                game.Platforms.Add(platformToAdd);
            }

            foreach (var platformToRemove in platformsToDelete)
            {
                game.Platforms.Remove(platformToRemove);
            }
        }
        private async Task CheckRelatedPublisher(int id, Game game)
        {
            var publisher = await _unitOfWork.GetRepository<Publisher, IPublisherRepository>().GetByIdAsync(id);
            if (publisher is null)
            {
                var supplier = await _suppliersRepository.GetByIdAsync(id);
                var publisherToAdd = new Publisher
                {
                    CompanyName = supplier.CompanyName,
                    Description = supplier.Address,
                    HomePage = supplier.HomePage,
                    FromMongo = supplier.CompanyName,
                    Games = null
                };
                await _unitOfWork.GetRepository<Publisher, IPublisherRepository>().AddAsync(publisherToAdd);
                await _unitOfWork.SaveAsync();
                game.PublisherId = publisherToAdd.Id;
                return;
            }
            game.PublisherId = publisher.Id;
        }

        public async Task AddNewComment(string key, CreateUpdateCommentModel model)
        {
            var game = await _unitOfWork.GetRepository<Game, IGameRepository>().GetByKeyAsync(key);
            if (game is null)
            {
                game = _mapper.Map<Game>(await _productRepository.GetByKeyAsync(key));
                await _unitOfWork.GetRepository<Game, IGameRepository>().AddAsync(game);
                await _unitOfWork.SaveAsync();
            }
            await _commentService.AddNewComment(key, model);
        }

        public async Task AddGameToBusket(string key)
        {
            var game = await _unitOfWork.GetRepository<Game, IGameRepository>().GetByKeyAsync(key);
            if (game is null)
            {
                game = _mapper.Map<Game>(await _productRepository.GetByKeyAsync(key));
                var newGame = await _productRepository.GetByIdAsync(game.Id);
                var category = await _categoryRepository.GetByIdAsync(newGame.CategoryID);
                var supplier = await _suppliersRepository.GetByIdAsync(newGame.SupplierID);

                var genreToAdd = await AddRelatedGenre(category);
                var publisherToAdd = await AddRelatedPublisher(supplier);

                var genreCategory = _mapper.Map<Genre>(category);
                var publisherSupplier = _mapper.Map<Publisher>(supplier);

                var genre = await _unitOfWork.GetRepository<Genre, IGenreRepository>().GetAllAsync(x => x.Name == genreCategory.Name);
                var publisher = await _unitOfWork.GetRepository<Publisher, IPublisherRepository>().GetByCompanyNameAsync(publisherSupplier.CompanyName);
                await _unitOfWork.GetRepository<Game, IGameRepository>().AddAsync(new Game
                {
                    Key = game.Key,
                    Name = game.Name,
                    Description = game.Description,
                    Price = game.Price,
                    UnitInStock = game.UnitInStock,
                    IsDeleted = false,
                    PublisherId = publisher.Id,
                    Publisher = publisher,
                    Genres = genre,
                    Discontinued = game.Discontinued,
                    Viewed = game.Viewed,
                    FromMongo = newGame.ProductName,
                    CreationDate = game.CreationDate,
                    Platforms = null
                });
                await _unitOfWork.SaveAsync();
            }
            await _basketService.AddGameToBasket(key);
        }

        public async Task RemoveGameFromBusket(string key)
        {
            await _basketService.RemoveGameFromBasket(key);
        }

        public async Task Download(string key)
        {
            var game = await _unitOfWork.GetRepository<Game, IGameRepository>().GetByKeyAsync(key);
            if (game is null)
            {
                return;
            }

            await _gameService.Download(key);
        }
        private string GenerateGameKey(CreateUpdateGameModel model)
        {
            return model.Name.ToLower().Replace(' ', '-');
        }
        private async Task UpdatePlatformsAsync(ICollection<int> updatePlatformsList, Game game)
        {
            var platforms = await _unitOfWork.GetRepository<Platform, IPlatformRepository>().GetAllAsync();

            var platformsToAdd = platforms.Where(genre => updatePlatformsList.Contains(genre.Id));

            var platformsToDelete = game.Platforms.Where(genre => !updatePlatformsList.Contains(genre.Id));

            foreach (var platformToAdd in platformsToAdd)
            {
                game.Platforms.Add(platformToAdd);
            }

            foreach (var platformToRemove in platformsToDelete)
            {
                game.Platforms.Remove(platformToRemove);
            }
        }
    }
}
