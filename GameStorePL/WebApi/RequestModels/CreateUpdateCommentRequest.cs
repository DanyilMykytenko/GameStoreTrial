using WebApi.Models;

namespace WebApi.RequestModels;

public class CreateUpdateCommentRequest
{
    public string Action { get; set; }

    public CommentDTO Comment { get; set; }

    public string ParentId { get; set; }
}
