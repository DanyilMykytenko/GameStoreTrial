using AutoMapper;
using Business.Models;
using WebApi.RequestModels;

namespace WebApi.Resolvers;

public class ResolveNullablePageCountIssue : IValueResolver<GetGamesRequest, GetGamesRequestModel, string?>
{
    public string? Resolve(GetGamesRequest source, GetGamesRequestModel destination, string? destMember, ResolutionContext context)
    {
        return !string.IsNullOrEmpty(source.PageCount) ? source.PageCount : "10";
    }
}