using Data.Data;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(GameStoreDbContext ctx) : base(ctx)
        {
        }

        public Task BanUser()
        {
            return Task.FromResult(0);
        }
    }
}
