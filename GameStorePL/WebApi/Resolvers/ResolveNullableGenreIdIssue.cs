using AutoMapper;
using Business.Models;
using WebApi.RequestModels;

namespace WebApi.Resolvers;

public class ResolveNullableGenreIdIssue : IValueResolver<CreateUpdateGenreRequest, CreateUpdateGenreModel, int?>
{
    public int? Resolve(CreateUpdateGenreRequest source, CreateUpdateGenreModel destination, int? destMember, ResolutionContext context)
    {
        return int.TryParse(source.Genre.Id, out int id) ? id : 0;
    }
}
