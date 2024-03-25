using AutoMapper;
using Business.Models;
using WebApi.RequestModels;

namespace WebApi.Resolvers;

public class ResolveParsePublisher : IValueResolver<CreateUpdateGameRequest, CreateUpdateGameModel, int?>
{
    public int? Resolve(CreateUpdateGameRequest source, CreateUpdateGameModel destination, int? destMember, ResolutionContext context)
    {
        return int.TryParse(source.Publisher, out int id) ? id : null;
    }
}
