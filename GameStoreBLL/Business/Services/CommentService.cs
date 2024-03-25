using AutoMapper;
using Business.Enums;
using Business.Interfaces;
using Business.Models;
using Business.Validation;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddAsync(CreateUpdateCommentModel model)
        {
            var comment = _mapper.Map<Comment>(model);
            await _unitOfWork.GetRepository<Comment, ICommentRepository>().AddAsync(comment);
            await _unitOfWork.SaveAsync();
        }

        public Task BanUser(int id)
        {
            return Task.FromResult(0);
        }

        public async Task DeleteAsync(int modelId)
        {
            if (modelId == 0)
            {
                throw new GameStoreException("Id is null");
            }

            var comment = await _unitOfWork.GetRepository<Comment, ICommentRepository>().GetByIdAsync(modelId);
            comment.isDeleted = true;
            comment.Body = $"<s>{comment.Body}</s>";

            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<CommentModel>> GetAllAsync()
        {
            var comments = await _unitOfWork.GetRepository<Comment, ICommentRepository>().GetAllAsync(x => !x.isDeleted);
            return _mapper.Map<IEnumerable<CommentModel>>(comments);
        }

        public async Task<CommentModel> GetByIdAsync(int id)
        {
            if (id == 0)
            {
                throw new GameStoreException("Id is null");
            }
            var comment = await _unitOfWork.GetRepository<Comment, ICommentRepository>().GetByIdAsync(id);
            if (comment is null)
            {
                throw new NotFoundException(nameof(comment));
            }
            return _mapper.Map<CommentModel>(comment);
        }

        public async Task UpdateAsync(CreateUpdateCommentModel model)
        {
            if (model is null)
            {
                throw new GameStoreException("Model is null");
            }
            var comment = await _unitOfWork.GetRepository<Comment, ICommentRepository>().GetByIdAsync(model.ParentId.Value);
            if (comment is null)
            {
                throw new NotFoundException(nameof(comment));
            }
            _mapper.Map(model, comment);
            _unitOfWork.GetRepository<Comment, ICommentRepository>().Update(comment);
            await _unitOfWork.SaveAsync();
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
        public async Task AddNewComment(string key, CreateUpdateCommentModel model)
        {
            var game = await _unitOfWork.GetRepository<Game, IGameRepository>().GetByKeyAsync(key);

            var comment = _mapper.Map<Comment>(model);
            comment.GameId = game.Id;

            switch (model.Action)
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

    }
}
