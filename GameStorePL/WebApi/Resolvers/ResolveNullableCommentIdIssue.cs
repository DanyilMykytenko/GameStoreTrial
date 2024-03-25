using AutoMapper;
using Business.Models;
using WebApi.RequestModels;

namespace WebApi.Resolvers;

public class ResolveNullableCommentIdIssue : IValueResolver<CreateUpdateCommentRequest, CreateUpdateCommentModel, int?>
{
    public int? Resolve(CreateUpdateCommentRequest source, CreateUpdateCommentModel destination, int? destMember, ResolutionContext context)
    {
        return int.TryParse(source.Comment.Id, out int id) ? id : 0;
    }
}
