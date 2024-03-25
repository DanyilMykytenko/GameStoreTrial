using AutoMapper;
using Business.Models;
using WebApi.RequestModels;

namespace WebApi.Resolvers;

public class ResolveNullablePlatformIssue : IValueResolver<CreateUpdatePlatformRequest, CreateUpdatePlatformModel, int?>
{
    public int? Resolve(CreateUpdatePlatformRequest source, CreateUpdatePlatformModel destination, int? destMember, ResolutionContext context)
    {
        return int.TryParse(source.Platform.Id, out int id) ? id : 0;
    }
}
