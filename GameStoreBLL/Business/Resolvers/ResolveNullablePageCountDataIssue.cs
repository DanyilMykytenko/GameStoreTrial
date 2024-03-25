using AutoMapper;
using Business.Models;
using Data.Models;

namespace Business.Resolvers
{
    public class ResolveNullablePageCountDataIssue : IValueResolver<GetGamesRequestModel, Filter, string?>
    {
        public string? Resolve(GetGamesRequestModel source, Filter destination, string? destMember, ResolutionContext context)
        {
            return !string.IsNullOrEmpty(source.PageCount) ? source.PageCount : "10";
        }
    }
}