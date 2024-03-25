using Data.Data;
using Data.Entities;
using Data.Enums;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class GameRepository : Repository<Game>, IGameRepository
{
    public GameRepository(GameStoreDbContext ctx)
        : base(ctx)
    {
    }

    public async Task<IEnumerable<Game>> GetByGenreAsync(int id)
    {
        var games = await _ctx.Set<Game>().Where(x => x.Genres.Any(y => y.Id == id)).ToListAsync();
        return games;
    }

    public async Task<Game> GetByKeyAsync(string key)
    {
        var games = await _ctx.Set<Game>().FirstOrDefaultAsync(x => x.Key == key);
        return games;
    }

    public async Task<IEnumerable<Game>> GetByPlatformAsync(int id)
    {
        var games = await _ctx.Set<Game>().Where(x => x.Platforms.Any(y => y.Id == id)).ToListAsync();
        return games;
    }
    public async Task<Game> GetByIdWithDetails(int id)
    {
        var game = await _ctx.Set<Game>()
            .Include(x => x.Genres)
            .Include(x => x.Platforms)
            .Include(x => x.Publisher)
            .FirstOrDefaultAsync(x => x.Id == id);
        return game;
    }

    public async Task<IEnumerable<Game>> GetByPublisherAsync(string companyName)
    {
        var games = await _ctx.Set<Game>().Where(x => x.Publisher.CompanyName == companyName).ToListAsync();
        return games;
    }

    public async Task<Game> GetByCommentAsync(int id)
    {
        var game = await _ctx.Set<Game>().Where(x => x.Comments.Any(y => y.Id == id)).FirstOrDefaultAsync();
        return game;
    }

    public async Task AddNewCommentAsync(string key, Comment comment)
    {
        var game = await _ctx.Set<Game>().FirstOrDefaultAsync(x => x.Key == key);
        comment.Game = game;
        _ctx.Set<Comment>().Add(comment);
    }

    public async Task<IEnumerable<Comment>> GetAllCommentsAsync(string key)
    {
        var game = await _ctx.Set<Game>().FirstOrDefaultAsync(x => x.Key == key);
        var comments = await _ctx.Set<Comment>().Where(x => x.Game == game).Include(x => x.Nested).ToListAsync();
        return comments;
    }

    public async Task AddNewReplyAsync(int parentId, Comment comment)
    {
        var parentComment = await _ctx.Set<Comment>().FirstOrDefaultAsync(x => x.Id == parentId);
        parentComment.Nested.Add(comment);
    }

    public async Task AddNewQuoteAsync(int parentId, Comment comment)
    {
        var parentComment = await _ctx.Set<Comment>().FirstOrDefaultAsync(x => x.Id == parentId);
    }

    public async Task AddNewComment(string action, string key, int parentId, Comment comment)
    {
        switch (action)
        {

        }
    }

    public async Task<IEnumerable<Game>> SortAsync(IQueryable<Game> query)
    {
        return await query.ToListAsync();
    }
    private IQueryable<Game> SortByDate(string date, IQueryable<Game> query)
    {
        switch (date)
        {
            case nameof(PublishedDateEnum.Week):
                query = query.Where(x => x.CreationDate == DateTime.Now.AddDays(-7));
                break;
            case nameof(PublishedDateEnum.Month):
                query = query.Where(x => x.CreationDate == DateTime.Now.AddMonths(-1));
                break;
            case nameof(PublishedDateEnum.Year):
                query = query.Where(x => x.CreationDate == DateTime.Now.AddYears(-1));
                break;
            case nameof(PublishedDateEnum.TwoYears):
                query = query.Where(x => x.CreationDate == DateTime.Now.AddYears(-2));
                break;
            case nameof(PublishedDateEnum.ThreeYears):
                query = query.Where(x => x.CreationDate == DateTime.Now.AddYears(-3));
                break;
        }
        return query;
    }
    private IQueryable<Game> Sort(string sort, IQueryable<Game> query)
    {
        switch (sort)
        {
            case nameof(SortEnum.Popular):
                query = query.OrderBy(x => x.Viewed);
                break;
            case nameof(SortEnum.Commented):
                query = query.OrderBy(x => x.Comments.Count());
                break;
            case nameof(SortEnum.Asc):
                query = query.OrderBy(x => x.Price);
                break;
            case nameof(SortEnum.Desc):
                query = query.OrderByDescending(x => x.Price);
                break;
            case nameof(SortEnum.New):
                query = query.OrderBy(x => x.CreationDate);
                break;
        }
        return query;
    }

    public IQueryable<Game> GetDbSet()
    {
        return _ctx.Set<Game>().Where(x => x.IsDeleted == false).AsQueryable();
    }
}
