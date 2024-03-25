using AutoMapper;
using Business.Models;
using Data.Models;

namespace Business.Resolvers
{
    internal class ResolveNullableMaxPriceIssue : IValueResolver<GetGamesRequestModel, Filter, int>
    {
        public int Resolve(GetGamesRequestModel source, Filter destination, int destMember, ResolutionContext context)
        {
            return source.MaxPrice == null ? source.MaxPrice.Value : 0;
        }
    }
}
