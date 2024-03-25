using AutoMapper;
using Business.Models;
using Business.Resolvers;
using Data.Entities;
using Data.Models;
using MongoData.Entities;

namespace Business;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<Game, GameModel>()
            .ForMember(
                dst => dst.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dst => dst.Name,
                opt => opt.MapFrom(src => src.Name))
            .ForMember(
                dst => dst.Key,
                opt => opt.MapFrom(src => src.Key))
            .ForMember(
                dst => dst.Description,
                opt => opt.MapFrom(src => src.Description))
            .ForMember(
                dst => dst.CreationDate,
                opt => opt.MapFrom(src => src.CreationDate))
            .ForMember(
                dst => dst.Viewed,
                opt => opt.MapFrom(src => src.Viewed))
            .ForMember(
                dst => dst.Genres,
                opt => opt.MapFrom(src => src.Genres))
            .ForMember(
                dst => dst.Platforms,
                opt => opt.MapFrom(src => src.Platforms))
            .ForMember(
                dst => dst.Publisher,
                opt => opt.MapFrom(src => src.Publisher))
            .ForMember(
                dst => dst.Comments,
                opt => opt.MapFrom(src => src.Comments))
            .ReverseMap();

        CreateMap<Genre, GenreModel>()
            .ForMember(
                dst => dst.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dst => dst.Name,
                opt => opt.MapFrom(src => src.Name))
            .ForMember(
                dst => dst.ParentId,
                opt => opt.MapFrom(src => src.ParentId))
            .ForMember(
                dst => dst.Parent,
                opt => opt.MapFrom(src => src.Parent))
            .ForMember(
                dst => dst.Nested,
                opt => opt.MapFrom(src => src.Nested))
            .ForMember(
                dst => dst.Games,
                opt => opt.MapFrom(src => src.Games))
            .ReverseMap();

        CreateMap<Platform, PlatformModel>()
            .ForMember(
                dst => dst.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dst => dst.Type,
                opt => opt.MapFrom(src => src.Type))
            .ForMember(
                dst => dst.Games,
                opt => opt.MapFrom(src => src.Games))
            .ReverseMap();

        CreateMap<Publisher, PublisherModel>()
            .ForMember(
                dst => dst.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dst => dst.CompanyName,
                opt => opt.MapFrom(src => src.CompanyName))
            .ForMember(
                dst => dst.Description,
                opt => opt.MapFrom(src => src.Description))
            .ForMember(
                dst => dst.HomePage,
                opt => opt.MapFrom(src => src.HomePage))
            .ForMember(
                dst => dst.Games,
                opt => opt.MapFrom(src => src.Games))
            .ReverseMap();

        CreateMap<Data.Entities.Order, OrderModel>()
            .ForMember(
                dst => dst.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dst => dst.isPaid,
                opt => opt.MapFrom(src => src.isPaid))
            .ForMember(
                dst => dst.CustomerId,
                opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(
                dst => dst.OrderDate,
                opt => opt.MapFrom(src => src.OrderDate))
            .ForMember(
                dst => dst.Price,
                opt => opt.MapFrom(src => src.Details.Sum(x => x.Price)))
            .ReverseMap();

        CreateMap<OrderDetails, OrderDetailsModel>()
            .ForMember(
                dst => dst.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dst => dst.ProductId,
                opt => opt.MapFrom(src => src.ProductId))
            .ForMember(
                dst => dst.Quantity,
                opt => opt.MapFrom(src => src.Quantity))
            .ForMember(
                dst => dst.Price,
                opt => opt.MapFrom(src => src.Price))
            .ForMember(
                dst => dst.Discount,
                opt => opt.MapFrom(src => src.Discount))
            .ReverseMap();

        CreateMap<Basket, BasketModel>()
            .ForMember(
                dst => dst.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dst => dst.Details,
                opt => opt.MapFrom(src => src.Details))
            .ForMember(
                dst => dst.CustomerId,
                opt => opt.MapFrom(src => src.CustomerId))
            .ReverseMap();

        CreateMap<Comment, CommentModel>()
            .ForMember(
                dst => dst.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dst => dst.Name,
                opt => opt.MapFrom(src => src.Author))
            .ForMember(
                dst => dst.Body,
                opt => opt.MapFrom(src => src.Body))
            .ForMember(
                dst => dst.Nested,
                opt => opt.MapFrom(src => src.Nested))
            .ForMember(
                dst => dst.ParentId,
                opt => opt.MapFrom(src => src.ParentId))
            .ForMember(
                dst => dst.Parent,
                opt => opt.MapFrom(src => src.Parent))
            .ForMember(
                dst => dst.Game,
                opt => opt.MapFrom(src => src.Game))
            .ReverseMap();

        CreateMap<CreateUpdateGameModel, Game>()
            .ForMember(
                src => src.Id,
                opt => opt.MapFrom(dst => dst.Id.Value))
            .ForMember(
                src => src.Name,
                opt => opt.MapFrom(dst => dst.Name))
            .ForMember(
                src => src.Key,
                opt => opt.MapFrom(dst => dst.Key))
            .ForMember(
                src => src.Description,
                opt => opt.MapFrom(dst => dst.Description))
            .ForMember(
                src => src.Price,
                opt => opt.MapFrom(dst => dst.Price))
            .ForMember(
                src => src.UnitInStock,
                opt => opt.MapFrom(dst => dst.UnitInStock))
            .ForMember(
                src => src.Discontinued,
                opt => opt.MapFrom(dst => dst.Discontinued))
            .ForMember(
                src => src.Genres,
                opt => opt.Ignore())
            .ForMember(
                src => src.Platforms,
                opt => opt.Ignore())
            .ForMember(
                src => src.Publisher,
                opt => opt.Ignore())
            .ReverseMap();

        CreateMap<CreateUpdateGenreModel, Genre>()
            .ForMember(
                src => src.Id,
                opt => opt.MapFrom(dst => dst.Id.Value))
            .ForMember(
                src => src.Name,
                opt => opt.MapFrom(dst => dst.Name))
            .ForMember(
                src => src.ParentId,
                opt => opt.MapFrom(dst => dst.ParentGenreId))
            .ReverseMap();

        CreateMap<CreateUpdatePublisherModel, Publisher>()
            .ForMember(
                src => src.Id,
                opt => opt.MapFrom(dst => dst.Id.Value))
            .ForMember(
                src => src.CompanyName,
                opt => opt.MapFrom(dst => dst.CompanyName))
            .ForMember(
                src => src.Description,
                opt => opt.MapFrom(dst => dst.Description))
            .ForMember(
                src => src.HomePage,
                opt => opt.MapFrom(dst => dst.HomePage))
            .ReverseMap();

        CreateMap<CreateUpdatePlatformModel, Platform>()
            .ForMember(
                src => src.Type,
                opt => opt.MapFrom(dst => dst.Type))
            .ForMember(
                src => src.Id,
                opt => opt.MapFrom(dst => dst.Id.Value))
            .ReverseMap();

        CreateMap<CreateUpdateOrderModel, Data.Entities.Order>()
            .ForMember(
                dst => dst.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dst => dst.isPaid,
                opt => opt.MapFrom(src => src.isPaid))
            .ForMember(
                dst => dst.CustomerId,
                opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(
                dst => dst.OrderDate,
                opt => opt.MapFrom(src => src.OrderDate))
            .ReverseMap();


        CreateMap<CreateUpdateCommentModel, Comment>()
            .ForMember(
                src => src.Author,
                opt => opt.MapFrom(dst => dst.Name))
            .ForMember(
                src => src.ParentId,
                opt => opt.MapFrom(dst => dst.ParentId))
            .ReverseMap();

        CreateMap<GetGamesRequestModel, Filter>()
            .ForMember(
                dst => dst.Name,
                opt => opt.MapFrom(src => src.Name))
            .ForMember(
                dst => dst.MaxPrice,
                opt => opt.MapFrom(src => src.MaxPrice))
            .ForMember(
                dst => dst.MinPrice,
                opt => opt.MapFrom(src => src.MinPrice))
            .ForMember(
                dst => dst.Page,
                opt => opt.MapFrom(src => src.Page))
            .ForMember(
                dst => dst.PageCount,
                opt => opt.MapFrom<ResolveNullablePageCountDataIssue>())
            .ForMember(
                dst => dst.DatePublishing,
                opt => opt.MapFrom(src => src.DatePublishing))
            .ForMember(
                dst => dst.Sort,
                opt => opt.MapFrom(src => src.Sort))
            .ForMember(
                dst => dst.Genres,
                opt => opt.MapFrom(src => src.Genres))
            .ForMember(
                dst => dst.Platforms,
                opt => opt.MapFrom(src => src.Platforms))
            .ForMember(
                dst => dst.Publishers,
                opt => opt.MapFrom(src => src.Publishers))
            .ReverseMap();


        CreateMap<Product, Game>()
            .ForMember(
                dst => dst.Id,
                opt => opt.MapFrom(src => src.ProductID))
            .ForMember(
                dst => dst.Name,
                opt => opt.MapFrom(src => src.ProductName))
            .ForMember(
                dst => dst.Price,
                opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(
                dst => dst.UnitInStock,
                opt => opt.MapFrom(src => src.UnitsInStock))
            .ForMember(
                dst => dst.Description,
                opt => opt.MapFrom(src => src.QuantityPerUnit))
            .ForMember(
                dst => dst.Discontinued,
                opt => opt.MapFrom(src => src.Discontinued))
            .ForMember(
                dst => dst.PublisherId,
                opt => opt.MapFrom(src => src.SupplierID))
            .ReverseMap();

        CreateMap<Category, Genre>()
            .ForMember(
                dst => dst.Name,
                opt => opt.MapFrom(src => src.CategoryName))
            .ForMember(
                dst => dst.Nested,
                opt => opt.Ignore())
            .ForMember(
                dst => dst.ParentId,
                opt => opt.Ignore())
            .ForMember(
                dst => dst.Parent,
                opt => opt.Ignore())
            .ForMember(
                dst => dst.Id,
                opt => opt.MapFrom(src => src.CategoryID))
            .ForMember(
                dst => dst.Games,
                opt => opt.Ignore())
            .ReverseMap();

        CreateMap<Supplier, Publisher>()
            .ForMember(
                dst => dst.Id,
                opt => opt.MapFrom(src => src.SupplierID))
            .ForMember(
                dst => dst.HomePage,
                opt => opt.MapFrom(src => src.HomePage))
            .ForMember(
                dst => dst.CompanyName,
                opt => opt.MapFrom(src => src.CompanyName))
            .ForMember(
                dst => dst.Description,
                opt => opt.MapFrom(src => src.Address))
            .ForMember(
                dst => dst.Games,
                opt => opt.Ignore())
            .ReverseMap();

        CreateMap<User, UserModel>()
            .ForMember(
                dst => dst.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dst => dst.UserName,
                opt => opt.MapFrom(src => src.UserName))
            .ForMember(
                dst => dst.Password,
                opt => opt.MapFrom(src => src.PasswordHash))
            .ReverseMap();

        CreateMap<Role, RoleModel>()
            .ForMember(
                dst => dst.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dst => dst.Name,
                opt => opt.MapFrom(src => src.Name))
            .ReverseMap();

        CreateMap<UpdateRoleModel, Role>()
            .ForMember(
                dst => dst.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dst => dst.Name,
                opt => opt.MapFrom(src => src.Name));
    }
}
