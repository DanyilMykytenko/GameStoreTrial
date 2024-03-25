using AutoMapper;
using Business.Models;
using WebApi.RequestModels;

namespace WebApi.Resolvers;

public class ResolveParseGenres : IValueResolver<CreateUpdateGameRequest, CreateUpdateGameModel, List<int>?>
{
    public List<int>? Resolve(CreateUpdateGameRequest source, CreateUpdateGameModel destination, List<int>? destMember, ResolutionContext context)
    {
        if (source.Genres != null)
        {
            var list = source.Genres.Select(src => int.TryParse(src, out int result) ? result : 0).ToList();
            return list;
        }

        return new List<int>();
    }
}