using AutoMapper;
using Business.Models;
using WebApi.Models;
using WebApi.RequestModels;
using WebApi.Resolvers;
using WebApi.ResponseModels;

namespace WebApi;

public class AutomapperPresentationProfile : Profile
{
    public AutomapperPresentationProfile()
    {
        CreateMap<GameDTO, GameModel>()
            .ForMember(
                src => src.Id,
                opt => opt.MapFrom(dst => int.Parse(dst.Id)))
            .ForMember(
                src => src.Key,
                opt => opt.MapFrom(dst => dst.Key))
            .ForMember(
                src => src.Name,
                opt => opt.MapFrom(dst => dst.Name))
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
            .ReverseMap();

        CreateMap<GenreDTO, GenreModel>()
            .ForMember(
                src => src.Name,
                opt => opt.MapFrom(dst => dst.Name))
            .ForMember(
                src => src.Id,
                opt => opt.MapFrom<ResolveNullableGenreDTOIssue>())
            .ForMember(
                src => src.ParentId,
                opt => opt.MapFrom<ResolveNullableGenreDTOParentIssue>())
            .ReverseMap();

        CreateMap<PlatformDTO, PlatformModel>()
            .ForMember(
                src => src.Id,
                opt => opt.MapFrom(dst => int.Parse(dst.Id)))
            .ForMember(
                src => src.Type,
                opt => opt.MapFrom(dst => dst.Type))
            .ReverseMap();

        CreateMap<PublisherDTO, PublisherModel>()
            .ForMember(
                src => src.Id,
                opt => opt.MapFrom(dst => int.Parse(dst.Id)))
            .ForMember(
                src => src.CompanyName,
                opt => opt.MapFrom(dst => dst.CompanyName))
            .ForMember(
                src => src.HomePage,
                opt => opt.MapFrom(dst => dst.HomePage))
            .ForMember(
                src => src.Description,
                opt => opt.MapFrom(dst => dst.Description))
            .ReverseMap();

        CreateMap<OrderModel, OrderDTO>()
            .ForMember(
                dst => dst.OrderId,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dst => dst.OrderDate,
                opt => opt.MapFrom(src => src.OrderDate))
            .ForMember(
                dst => dst.UserId,
                opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(
                dst => dst.Sum,
                opt => opt.MapFrom(src => src.Price))
            .ReverseMap();

        CreateMap<OrderModel, OrderDetailsDTO>()
            .ForMember(
                dst => dst.ProductId,
                opt => opt.MapFrom(src => src.ProductId))
            .ForMember(
                dst => dst.Quantity,
                opt => opt.MapFrom(src => src.Quantity))
            .ForMember(
                dst => dst.Discount,
                opt => opt.MapFrom(src => src.Discount))
            .ForMember(
                dst => dst.Price,
                opt => opt.MapFrom(src => src.Price))
            .ReverseMap();

        CreateMap<OrderDetailsModel, OrderDetailsDTO>()
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

        CreateMap<BusketDTO, BasketModel>()
            .ForMember(
                src => src.Id,
                opt => opt.MapFrom(dst => dst.Id))
            .ForMember(
                src => src.Details,
                opt => opt.MapFrom(dst => dst.Details))
            .ForMember(
                src => src.CustomerId,
                opt => opt.MapFrom(dst => dst.CustomerId))
            .ReverseMap();

        CreateMap<PaymentDTO, VisaTransactionModel>()
            .ForMember(
                dst => dst.TransactionAmount,
                opt => opt.MapFrom(src => src.TransactionAmount))
            .ForMember(
                dst => dst.CardholderName,
                opt => opt.MapFrom(src => src.CardholderName))
            .ForMember(
                dst => dst.CardNumber,
                opt => opt.MapFrom(src => src.CardNumber))
            .ForMember(
                dst => dst.ExpirationMonth,
                opt => opt.MapFrom(src => src.ExpirationMonth))
            .ForMember(
                dst => dst.CVV,
                opt => opt.MapFrom(src => src.Cvv2))
            .ForMember(
                dst => dst.ExpirationYear,
                opt => opt.MapFrom(src => src.ExpirationYear))
            .ReverseMap();

        CreateMap<PaymentDTO, IBoxTransactionModel>()
            .ForMember(
                dst => dst.TransactionAmount,
                opt => opt.MapFrom(src => src.TransactionAmount))
            .ForMember(
                dst => dst.AccountNumber,
                opt => opt.MapFrom(src => src.AccountNumber))
            .ForMember(
                dst => dst.InvoiceNumber,
                opt => opt.MapFrom(src => src.InvoiceNumber))
            .ReverseMap();

        CreateMap<VisaPaymentDTO, VisaTransactionModel>()
            .ForMember(
                dst => dst.TransactionAmount,
                opt => opt.MapFrom(src => src.TransactionAmount))
            .ForMember(
                dst => dst.CardholderName,
                opt => opt.MapFrom(src => src.Holder))
            .ForMember(
                dst => dst.CardNumber,
                opt => opt.MapFrom(src => src.CardNumber))
            .ForMember(
                dst => dst.ExpirationMonth,
                opt => opt.MapFrom(src => src.MonthExpire))
            .ForMember(
                dst => dst.CVV,
                opt => opt.MapFrom(src => src.Cvv2))
            .ForMember(
                dst => dst.ExpirationYear,
                opt => opt.MapFrom(src => src.YearExpire))
            .ReverseMap();

        CreateMap<CommentModel, CommentDTO>()
            .ForMember(
                dst => dst.Id,
                opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(
                dst => dst.Name,
                opt => opt.MapFrom(src => src.Name))
            .ForMember(
                dst => dst.Body,
                opt => opt.MapFrom(src => src.Body))
            .ForMember(
                dst => dst.ChildComments,
                opt => opt.MapFrom(src => src.Nested))
            .ReverseMap();

        CreateMap<CreateUpdateGameRequest, CreateUpdateGameModel>()
            .ForMember(
                src => src.Id,
                opt => opt.MapFrom<ResolveNullableGameIssue>())
            .ForMember(
                src => src.Name,
                opt => opt.MapFrom(dst => dst.Game.Name))
            .ForMember(
                src => src.Description,
                opt => opt.MapFrom(dst => dst.Game.Description))
            .ForMember(
                src => src.Key,
                opt => opt.MapFrom(dst => dst.Game.Key))
            .ForMember(
                src => src.Price,
                opt => opt.MapFrom(dst => dst.Game.Price))
            .ForMember(
                src => src.UnitInStock,
                opt => opt.MapFrom(dst => dst.Game.UnitInStock))
            .ForMember(
                src => src.Discontinued,
                opt => opt.MapFrom(dst => dst.Game.Discontinued))
            .ForMember(
                src => src.Publisher,
                opt => opt.MapFrom<ResolveParsePublisher>())
            .ForMember(
                src => src.Genres,
                opt => opt.MapFrom<ResolveParseGenres>())
            .ForMember(
                src => src.Platforms,
                opt => opt.MapFrom<ResolveParsePlatforms>());

        CreateMap<CreateUpdateGenreRequest, CreateUpdateGenreModel>()
            .ForMember(
                src => src.Id,
                opt => opt.MapFrom<ResolveNullableGenreIdIssue>())
            .ForMember(
                src => src.ParentGenreId,
                opt => opt.MapFrom<ResolveNullableGenreParentIdIssue>())
            .ForMember(
                src => src.Name,
                opt => opt.MapFrom(dst => dst.Genre.Name));

        CreateMap<CreateUpdatePlatformRequest, CreateUpdatePlatformModel>()
            .ForMember(
                src => src.Id,
                opt => opt.MapFrom<ResolveNullablePlatformIssue>())
            .ForMember(
                src => src.Type,
                opt => opt.MapFrom(dst => dst.Platform.Type));

        CreateMap<CreateUpdatePublisherRequest, CreateUpdatePublisherModel>()
            .ForMember(
                src => src.Id,
                opt => opt.MapFrom<ResolveNullablePublisherIdIssue>())
            .ForMember(
                src => src.HomePage,
                opt => opt.MapFrom(dst => dst.Publisher.HomePage))
            .ForMember(
                src => src.Description,
                opt => opt.MapFrom(dst => dst.Publisher.Description))
            .ForMember(
                src => src.CompanyName,
                opt => opt.MapFrom(dst => dst.Publisher.CompanyName));

        CreateMap<CreateUpdateCommentRequest, CreateUpdateCommentModel>()
            .ForMember(
                dst => dst.Action,
                opt => opt.MapFrom(src => src.Action))
            .ForMember(
                dst => dst.Name,
                opt => opt.MapFrom(src => src.Comment.Name))
            .ForMember(
                dst => dst.Body,
                opt => opt.MapFrom(src => src.Comment.Body))
            .ForMember(
                dst => dst.ParentId,
                opt => opt.MapFrom<ResolveNullableCommentParentIdIssue>())
            .ReverseMap();

        CreateMap<GetGamesRequest, GetGamesRequestModel>()
            .ForMember(
                dst => dst.Name,
                opt => opt.MapFrom(src => src.Name))
            .ForMember(
                dst => dst.MaxPrice,
                opt => opt.MapFrom<ResolveNullableMaxPriceIssue>())
            .ForMember(
                dst => dst.MinPrice,
                opt => opt.MapFrom<ResolveNullableMinPriceIssue>())
            .ForMember(
                dst => dst.Page,
                opt => opt.MapFrom(src => src.Page))
            .ForMember(
                dst => dst.PageCount,
                opt => opt.MapFrom<ResolveNullablePageCountIssue>())
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

        CreateMap<GetGamesResponse, GetGamesResponseModel>()
            .ForMember(
                dst => dst.Games,
                opt => opt.MapFrom(src => src.Games))
            .ForMember(
                dst => dst.CurrentPage,
                opt => opt.MapFrom(src => src.CurrentPage))
            .ForMember(
                dst => dst.TotalPages,
                opt => opt.MapFrom(src => src.TotalPages))
            .ReverseMap();

        CreateMap<HistoryRequestModel, HistoryModel>()
            .ForMember(
                dst => dst.Start,
                opt => opt.MapFrom(src => src.Start))
            .ForMember(
                dst => dst.End,
                opt => opt.MapFrom(src => src.End))
            .ReverseMap();

        CreateMap<JWTModel, JWTBusinessModel>()
            .ForMember(
                dst => dst.UserId,
                opt => opt.MapFrom(src => src.UserId))
            .ForMember(
                dst => dst.RoleName,
                opt => opt.MapFrom(src => src.RoleName))
            .ReverseMap();

        CreateMap<UserModel, GetUserRequest>()
            .ForMember(
                dst => dst.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dst => dst.Name,
                opt => opt.MapFrom(src => src.UserName))
            .ForMember(
                dst => dst.Roles,
                opt => opt.MapFrom(src => src.Roles))
            .ForMember(
                dst => dst.Password,
                opt => opt.MapFrom(src => src.Password))
            .ReverseMap();

        CreateMap<CreateUserRequest, UserModel>()
            .ForMember(
                dst => dst.Id,
                opt => opt.MapFrom(src => src.User.Id))
            .ForMember(
                dst => dst.UserName,
                opt => opt.MapFrom(src => src.User.Name))
            .ForMember(
                dst => dst.Roles,
                opt => opt.MapFrom(src => src.Roles))
            .ForMember(
                dst => dst.Password,
                opt => opt.MapFrom(src => src.Password));

        CreateMap<UpdateUserRequest, UserModel>()
            .ForMember(
                dst => dst.Id,
                opt => opt.MapFrom(src => src.User.Id))
            .ForMember(
                dst => dst.UserName,
                opt => opt.MapFrom(src => src.User.Name))
            .ForMember(
                dst => dst.Roles,
                opt => opt.MapFrom(src => src.Roles));

        CreateMap<CreateRoleRequest, CreateRoleModel>()
            .ForMember(
                dst => dst.Name,
                opt => opt.MapFrom(src => src.Name))
            .ForMember(
                dst => dst.Claims,
                opt => opt.MapFrom(src => src.Claims));

        CreateMap<UpdateRoleRequest, UpdateRoleModel>()
            .ForMember(
                dst => dst.Id,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dst => dst.Name,
                opt => opt.MapFrom(src => src.Name))
            .ForMember(
                dst => dst.Claims,
                opt => opt.MapFrom(src => src.Claims));
    }
}
