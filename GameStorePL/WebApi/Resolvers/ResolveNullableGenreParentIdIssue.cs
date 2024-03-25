using AutoMapper;
using Business.Models;
using WebApi.RequestModels;

namespace WebApi.Resolvers;

public class ResolveNullableGenreParentIdIssue : IValueResolver<CreateUpdateGenreRequest, CreateUpdateGenreModel, int?>
{
    public int? Resolve(CreateUpdateGenreRequest source, CreateUpdateGenreModel destination, int? destMember, ResolutionContext context)
    {
        return int.TryParse(source.Genre.ParentGenreId, out int id) ? id : null;
    }
}
