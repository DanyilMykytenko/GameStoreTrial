using AutoMapper;
using Business.Models;
using WebApi.RequestModels;

namespace WebApi.Resolvers;

public class ResolveNullablePublisherIdIssue : IValueResolver<CreateUpdatePublisherRequest, CreateUpdatePublisherModel, int?>
{
    public int? Resolve(CreateUpdatePublisherRequest source, CreateUpdatePublisherModel destination, int? destMember, ResolutionContext context)
    {
        return int.TryParse(source.Publisher.Id, out int id) ? id : 0;
    }
}
