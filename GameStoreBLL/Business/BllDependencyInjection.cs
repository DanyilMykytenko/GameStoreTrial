using Business.Builders;
using Business.Infrastructure.Interfaces;
using Business.Infrastructure.Services;
using Business.Interfaces;
using Business.PaymentStrategies;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Business;

public static class BllDependencyInjection
{
    public static void AddBusinessLogicLayer(
        this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutomapperProfile));
        services.AddScoped<IGameService, GameService>();
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<IPlatformService, PlatformService>();
        services.AddScoped<IPublisherService, PublisherService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IBasketService, BasketService>();
        services.AddScoped<ISeedService, SeedService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IGameProductService, GameProductService>();
        services.AddScoped<IGenreCategoryService, GenreCategoryService>();
        services.AddScoped<IPublisherSupplierService, PublisherSupplierService>();
        services.AddScoped<IPaymentFactory, PaymentFactory>();
        services.AddScoped<IJWTGeneratorService, JWTGeneratorService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();

        services.AddSingleton<QueryDirector>();
        services.AddSingleton<MongoDirector>();
        services.AddSingleton<BusinessDirector>();


    }
}
