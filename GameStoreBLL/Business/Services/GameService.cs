using AutoMapper;
using Business.Builders;
using Business.Enums;
using Business.Interfaces;
using Business.Models;
using Business.Validation;
using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Data.Models;
using System.Text;

namespace Business.Services;

public class GameService : IGameService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly GameStoreDbContext _context;
    private readonly IMapper _mapper;
    private static string pageCount = "10";
    private const int customerId = 1;

    public GameService(IUnitOfWork unitOfWork, IMapper mapper, GameStoreDbContext context)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _context = context;
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

        if(string.IsNullOrEmpty(model.Key))
        {
            model.Key = GenerateGameKey(model);
        }
        var game = _mapper.Map<Game>(model);

        var genres = await _unitOfWork.GetRepository<Genre, IGenreRepository>().GetAllAsync(x => model.Genres.Contains(x.Id));
        game.Genres = genres;

        var platforms = await _unitOfWork.GetRepository<Platform, IPlatformRepository>().GetAllAsync(x => model.Platforms.Contains(x.Id));
        game.Platforms = platforms;

        await SetPublisherAsync(model.Publisher, game);

        game.CreationDate = DateTime.UtcNow;

        await _unitOfWork.GetRepository<Game, IGameRepository>().AddAsync(game);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(int modelId)
    {
        if (modelId == 0)
        {
            throw new GameStoreException("ModelId is null!");
        }

        var game = await _unitOfWork.GetRepository<Game, IGameRepository>().GetByIdAsync(modelId);
        await RemoveRelatedData(game.Platforms, game.Genres, game.Publisher, game);

        await _unitOfWork.GetRepository<Game, IGameRepository>().DeleteByIdAsync(modelId);
        await _unitOfWork.SaveAsync();
    }

    public async Task Download(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new GameStoreException("Key is null!");
        }
        var game = await _unitOfWork.GetRepository<Game, IGameRepository>().GetByKeyAsync(key);
        if(game is null)
        {
            throw new NotFoundException(nameof(game));
        }
        string path = Path.Combine(Directory.GetCurrentDirectory(), $"{game.Key}.txt");
        using FileStream fs = System.IO.File.Create(path);
        var text = Encoding.ASCII.GetBytes($"{game.Name}: {game.Key}");
        await fs.WriteAsync(text);
    }

    public async Task<GetGamesResponseModel> GetAllAsync(GetGamesRequestModel model)
    {
        var sort = _mapper.Map<Filter>(model);

        var director = new QueryDirector();
        var query = director.Sort(sort);

        var games = await _unitOfWork.GetRepository<Game, IGameRepository>().GetAllAsync(query);

        if (sort.PageCount == PaggingEnum.All.ToString())
        {
            sort.PageCount = games.Count().ToString();
        }

        var result = new GetGamesResponseModel
        {
            Games = _mapper.Map<ICollection<GameModel>>(games),
            CurrentPage = model.Page.Value,
            TotalPages = (games.Count() / int.Parse(sort.PageCount)).ToString()
        };
        return _mapper.Map<GetGamesResponseModel>(result);
    }

    public async Task<IEnumerable<GameModel>> GetAllAsync()
    {
        var games = await _unitOfWork.GetRepository<Game, IGameRepository>().GetAllAsync();
        return _mapper.Map<IEnumerable<GameModel>>(games);
    }

    public async Task<IEnumerable<GameModel>> GetByGenreAsync(int genreId)
    {
        if (genreId == 0)
        {
            throw new GameStoreException("There is no such genre!");
        }
        var games = await _unitOfWork.GetRepository<Game, IGameRepository>().GetByGenreAsync(genreId);
        if (games is null)
        {
            throw new NotFoundException(nameof(games));
        }

        return _mapper.Map<IEnumerable<GameModel>>(games);
    }

    public async Task<GameModel> GetByIdAsync(int id)
    {
        if (id == 0)
        {
            throw new GameStoreException("Id is null!");
        }

        var game = await _unitOfWork.GetRepository<Game, IGameRepository>().GetByIdAsync(id);
        if (game is null)
        {
            throw new NotFoundException(nameof(game));
        }
        game.Viewed += 1;
        await _unitOfWork.SaveAsync();
        return _mapper.Map<GameModel>(game);
    }

    public async Task<GameModel> GetByKeyAsync(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new GameStoreException("Key is null!");
        }

        var game = await _unitOfWork.GetRepository<Game, IGameRepository>().GetByKeyAsync(key);
        if (game is null)
        {
            throw new NotFoundException(nameof(game));
        }

        await UpdateGameViewed(game);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<GameModel>(game);
    }

    public async Task<IEnumerable<GameModel>> GetByPlatformAsync(int platformId)
    {
        if (platformId == 0)
        {
            throw new GameStoreException("There is no such genre!");
        }
        var games = await _unitOfWork.GetRepository<Game, IGameRepository>().GetByPlatformAsync(platformId);
        if (games == null)
        {
            throw new NotFoundException(nameof(games));
        }
        return _mapper.Map<IEnumerable<GameModel>>(games);
    }

    public async Task UpdateAsync(CreateUpdateGameModel model)
    {
        if (model is null)
        {
            throw new GameStoreException("Model is null!");
        }

        if(string.IsNullOrEmpty(model.Key))
        {
            model.Key = GenerateGameKey(model);
        }
        var game = await _unitOfWork.GetRepository<Game, IGameRepository>().GetByIdWithDetails(model.Id.Value);
        if (game is null)
        {
            throw new NotFoundException(nameof(game));
        }
        _mapper.Map(model, game);

        await UpdateGenresAsync(model.Genres, game);
        await UpdatePlatformsAsync(model.Platforms, game);
        await SetPublisherAsync(model.Publisher, game);

        _unitOfWork.GetRepository<Game, IGameRepository>().Update(game);
        await _unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<GameModel>> GetByPublisherAsync(string companyName)
    {
        if(string.IsNullOrEmpty(companyName))
        {
            throw new GameStoreException("There is no such publisher");
        }
        var games = await _unitOfWork.GetRepository<Game, IGameRepository>().GetByPublisherAsync(companyName);
        if(games == null)
        {
            throw new NotFoundException(nameof(games));
        }
        return _mapper.Map<IEnumerable<GameModel>>(games);
    }
    public async Task<GameModel> GetByCommentAsync(int id)
    {
        var game = await _unitOfWork.GetRepository<Game, IGameRepository>().GetByCommentAsync(id);
        return _mapper.Map<GameModel>(game);
    }

    public async Task AddNewCommentAsync(string key, CreateUpdateCommentModel model)
    {
        var comment = _mapper.Map<Comment>(model);
        comment.ParentId = null;
        await _unitOfWork.GetRepository<Game, IGameRepository>().AddNewCommentAsync(key, comment);
        await _unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<CommentModel>> GetAllCommentsAsync(string key)
    {
        var comments = await _unitOfWork.GetRepository<Game, IGameRepository>().GetAllCommentsAsync(key);
        return _mapper.Map<IEnumerable<CommentModel>>(comments.Where(x => x.Parent is null));
    }

    public async Task AddNewReplyAsync(int parentId, CommentModel model)
    {
        var comment = _mapper.Map<Comment>(model);
        await _unitOfWork.GetRepository<Game, IGameRepository>().AddNewReplyAsync(parentId, comment);
        await _unitOfWork.SaveAsync();
    }

    public async Task AddGameToBasket(string key)
    {
        var game = await _unitOfWork.GetRepository<Game, IGameRepository>().GetByKeyAsync(key);
        var order = new OrderDetails
        {
            Price = game.Price,
            Discount = game.Discontinued,
            Quantity = 1,
            ProductId = game.Id.ToString()
        };
        var basket = await _unitOfWork.GetRepository<Basket, IBasketRepository>().GetByIdWithDetails(1);
        if (basket is null)
        {
            await _unitOfWork.GetRepository<Basket, IBasketRepository>().AddAsync(
                new Basket
                {
                    Details = new List<OrderDetails>()
                    {
                            order
                    },
                    CustomerId = 1
                });
            await _unitOfWork.SaveAsync();
            return;
        }

        var detail = basket.Details.FirstOrDefault(x => x.ProductId == game.Id.ToString());
        if (detail is null)
        {
            basket.Details.Add(order);
        }
        else
        {
            basket.Details.FirstOrDefault(x => x.ProductId == game.Id.ToString()).Quantity += 1;
        }
        _unitOfWork.GetRepository<Basket, IBasketRepository>().Update(basket);
        await _unitOfWork.SaveAsync();
    }

    public async Task RemoveGameFrombasket(string key)
    {
        var game = await _unitOfWork.GetRepository<Game, IGameRepository>().GetByKeyAsync(key);
        var basket = await _unitOfWork.GetRepository<Basket, IBasketRepository>().GetByIdWithDetails(1);


        var details = basket.Details.First(x => x.ProductId == game.Id.ToString());
        basket.Details.Remove(details);
        await _unitOfWork.GetRepository<OrderDetails, IOrderDetailsRepository>().DeleteByIdAsync(details.Id);
        await _unitOfWork.SaveAsync();
    }

    public async Task AddNewComment(string key, CreateUpdateCommentModel model)
    {
        var game = await _unitOfWork.GetRepository<Game, IGameRepository>().GetByKeyAsync(key);

        var comment = _mapper.Map<Comment>(model);
        comment.GameId = game.Id;

        switch(model.Action)
        {
            case nameof(CommentActionEnum.Reply):
                var replyParent = await _unitOfWork.GetRepository<Comment, ICommentRepository>().GetByIdAsync(comment.ParentId.Value);
                comment.Body = $"<p><b>{replyParent.Author}:</b> {comment.Body}</p>";
                comment.Parent = replyParent;
                break;
            case nameof(CommentActionEnum.Quote):
                var quoteParent = await _unitOfWork.GetRepository<Comment, ICommentRepository>().GetByIdAsync(comment.ParentId.Value);
                comment.Body = $"<p><i>{quoteParent.Author}</i> {comment.Body}</p>";
                comment.Parent = quoteParent;
                break;
        }

        await _unitOfWork.GetRepository<Comment, ICommentRepository>().AddAsync(comment);
        await _unitOfWork.SaveAsync();
    }

    private string GenerateGameKey(CreateUpdateGameModel model)
    {
        return model.Name.ToLower().Replace(' ', '-');
    }

    private async Task UpdateGenresAsync(ICollection<int> updateGenresList, Game game)
    {
        var genres = await _unitOfWork.GetRepository<Genre, IGenreRepository>().GetAllAsync();

        var genresToAdd = genres.Where(genre => updateGenresList.Contains(genre.Id));

        var genresToDelete = game.Genres.Where(genre => !updateGenresList.Contains(genre.Id));

        foreach (var genreToAdd in genresToAdd)
        {
            game.Genres.Add(genreToAdd);
        }

        foreach (var genreToRemove in genresToDelete)
        {
            game.Genres.Remove(genreToRemove);
        }
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
    private async Task RemoveRelatedData(ICollection<Platform> platformList, ICollection<Genre> genreList, Publisher publisherModel, Game game)
    {
        var platforms = await _unitOfWork.GetRepository<Platform, IPlatformRepository>().GetAllAsync(x => platformList.Contains(x));
        var genres = await _unitOfWork.GetRepository<Genre, IGenreRepository>().GetAllAsync(x => genreList.Contains(x));
        if (publisherModel is not null)
        {
            var publisher = await _unitOfWork.GetRepository<Publisher, IPublisherRepository>().GetByIdAsync(publisherModel.Id);
        }

        foreach (var removePlatform in platforms)
        {
            game.Platforms.Remove(removePlatform);
        }
        foreach (var removeGenre in genres)
        {
            game.Genres.Remove(removeGenre);
        }

        var basket = await _unitOfWork.GetRepository<Basket, IBasketRepository>().GetByIdWithDetails(1);
        foreach(var order in basket.Details)
        {
            if(order.ProductId == game.Id.ToString())
            {
                basket.Details.Remove(order);
                await _unitOfWork.GetRepository<OrderDetails, IOrderDetailsRepository>().DeleteByIdAsync(order.Id);
            }
        }

        game.Publisher = null;
    }
    private async Task SetPublisherAsync(int? publisherId, Game game)
    {
        if (publisherId != null)
        {
            game.Publisher = await _unitOfWork.GetRepository<Publisher, IPublisherRepository>().GetByIdAsync(publisherId.Value);
        }
        else
        {
            game.Publisher = null;
        }
    }
    private async Task UpdateGameViewed(Game game)
    {
        game.Viewed += 1;
        await _unitOfWork.SaveAsync();
    }
}
