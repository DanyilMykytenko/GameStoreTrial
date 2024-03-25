using Data.Entities;

namespace Data.Interfaces;

public interface IGameRepository : IRepository<Game>
{
    Task<Game> GetByKeyAsync(string key);

    Task<IEnumerable<Game>> GetByGenreAsync(int id);

    Task<IEnumerable<Game>> GetByPlatformAsync(int id);
    Task<IEnumerable<Game>> GetByPublisherAsync(string companyName);
    Task<Game> GetByIdWithDetails(int id);
    Task<Game> GetByCommentAsync(int id);
    Task AddNewCommentAsync(string key, Comment comment);
    Task<IEnumerable<Comment>> GetAllCommentsAsync(string key);
    Task AddNewReplyAsync(int parentId, Comment comment);
    Task AddNewQuoteAsync(int parentId, Comment comment);
    Task<IEnumerable<Game>> SortAsync(IQueryable<Game> query);
    IQueryable<Game> GetDbSet();
}
