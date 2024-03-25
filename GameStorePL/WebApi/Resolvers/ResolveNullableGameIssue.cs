using AutoMapper;
using Business.Models;
using WebApi.RequestModels;

namespace WebApi.Resolvers;

public class ResolveNullableGameIssue : IValueResolver<CreateUpdateGameRequest, CreateUpdateGameModel, int?>
{
    public int? Resolve(CreateUpdateGameRequest source, CreateUpdateGameModel destination, int? destMember, ResolutionContext context)
    {
        return int.TryParse(source.Game.Id, out int id) ? id : 0;
    }
}
