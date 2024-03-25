using Business.Models;

namespace Business.Interfaces
{
    public interface ICommentService : ICrudService<CommentModel, CreateUpdateCommentModel>
    {
        Task BanUser(int id);
        Task<GameModel> GetByCommentAsync(int id);
        Task AddNewCommentAsync(string key, CreateUpdateCommentModel model);
        Task<IEnumerable<CommentModel>> GetAllCommentsAsync(string key);
        Task AddNewReplyAsync(int parentId, CommentModel comment);
        Task AddNewComment(string key, CreateUpdateCommentModel model);
    }
}
