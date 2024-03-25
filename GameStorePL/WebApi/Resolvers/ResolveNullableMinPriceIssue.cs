using AutoMapper;
using Business.Models;
using WebApi.RequestModels;

namespace WebApi.Resolvers;

public class ResolveNullableMinPriceIssue : IValueResolver<GetGamesRequest, GetGamesRequestModel, int?>
{
    public int? Resolve(GetGamesRequest source, GetGamesRequestModel destination, int? destMember, ResolutionContext context)
    {
        return int.TryParse(source.MinPrice, out int minPrice) ? minPrice : null;
    }
}
