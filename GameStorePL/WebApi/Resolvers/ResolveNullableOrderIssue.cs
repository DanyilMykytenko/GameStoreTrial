// using AutoMapper;
// using Business.Models;
// using WebApi.RequestModels;
//
// namespace WebApi.Resolvers;

// public class ResolveNullableOrderIssue : IValueResolver<CreateUpdateOrderRequest, CreateUpdateOrderModel, int?>
// {
//     public int? Resolve(CreateUpdateOrderRequest source, CreateUpdateOrderModel destination, int? destMember, ResolutionContext context)
//     {
//         return int.TryParse(source.Order.Id, out int id) ? id : 0;
//     }
// }
