using AutoMapper;
using Business.Models;
using WebApi.Models;

namespace WebApi.Resolvers;

public class ResolveNullableGenreDTOIssue : IValueResolver<GenreDTO, GenreModel, int>
{
    public int Resolve(GenreDTO source, GenreModel destination, int destMember, ResolutionContext context)
    {
        return int.TryParse(source.Id, out int id) ? id : 0;
    }
}
