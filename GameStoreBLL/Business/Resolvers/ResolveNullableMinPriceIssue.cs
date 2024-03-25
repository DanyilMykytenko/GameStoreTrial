using AutoMapper;
using Business.Models;
using Data.Models;

namespace Business.Resolvers
{
    public class ResolveNullableMinPriceIssue : IValueResolver<GetGamesRequestModel, Filter, int>
    {
        public int Resolve(GetGamesRequestModel source, Filter destination, int destMember, ResolutionContext context)
        {
            return source.MinPrice == null ? source.MinPrice.Value : 0;
        }
    }
}
