using AutoMapper;
using Business.Models;
using WebApi.Models;

namespace WebApi.Resolvers;

public class ResolveNullableGenreDTOParentIssue : IValueResolver<GenreDTO, GenreModel, int?>
{
    public int? Resolve(GenreDTO source, GenreModel destination, int? destMember, ResolutionContext context)
    {
        return int.TryParse(source.ParentGenreId, out int id) ? id : 0;
    }
}
