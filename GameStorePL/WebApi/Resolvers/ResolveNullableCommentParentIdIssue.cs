using AutoMapper;
using Business.Models;
using WebApi.RequestModels;

namespace WebApi.Resolvers;

public class ResolveNullableCommentParentIdIssue : IValueResolver<CreateUpdateCommentRequest, CreateUpdateCommentModel, int?>
{
    public int? Resolve(CreateUpdateCommentRequest source, CreateUpdateCommentModel destination, int? destMember, ResolutionContext context)
    {
        return int.TryParse(source.ParentId, out int id) ? id : null;
    }
}
