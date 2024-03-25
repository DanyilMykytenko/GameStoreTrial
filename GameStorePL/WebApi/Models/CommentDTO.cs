namespace WebApi.Models;

public class CommentDTO
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Body { get; set; }

    public ICollection<CommentDTO>? ChildComments { get; set; }
}
