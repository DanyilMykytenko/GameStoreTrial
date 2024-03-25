// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Text;
using Business;
using Data;
using Data.Data;
using Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using MongoData;
using Serilog;
using Serilog.Events;
using WebApi;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("GameStoreLogs-.txt", restrictedToMinimumLevel: LogEventLevel.Information)
    .WriteTo.File("GameStoreErrorLogs-.txt", restrictedToMinimumLevel: LogEventLevel.Error)
    .CreateLogger();

IConfiguration configuration = builder.Configuration;

builder.Services.AddDataAccessLayer(configuration);
builder.Services.AddMongoDataAccessLayer(configuration);
builder.Services.AddBusinessLogicLayer();
builder.Services.AddAutoMapper(typeof(AutomapperPresentationProfile));
builder.Services.AddResponseCaching();

builder.Services.AddIdentity<User, Role>(config =>
{
    config.Password.RequiredLength = 4;
    config.Password.RequireDigit = false;
    config.Password.RequireNonAlphanumeric = false;
    config.Password.RequireUppercase = false;
    config.Password.RequireLowercase = false;
})
    .AddEntityFrameworkStores<GameStoreDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    if (configuration != null)
    {
        opt.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["JWT:Issuer"],
            ValidAudience = configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"].ToString())),
        };
    }
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(
        GlobalConstants.OrderCreatePolicy, policy => policy.RequireClaim("permission", GlobalConstants.OrderCreatePermission));
    options.AddPolicy(
        GlobalConstants.OrderGetPolicy, policy => policy.RequireClaim("permission", GlobalConstants.OrderGetPermission));
    options.AddPolicy(
        GlobalConstants.OrderUpdatePolicy, policy => policy.RequireClaim("permission", GlobalConstants.OrderUpdatePermission));
    options.AddPolicy(
        GlobalConstants.OrderDetailsUpdatePolicy, policy => policy.RequireClaim("permission", GlobalConstants.OrderDetailsUpdatePermission));
    options.AddPolicy(
        GlobalConstants.OrderDetailsDeletePolicy, policy => policy.RequireClaim("permission", GlobalConstants.OrderDetailsDeletePermission));
    options.AddPolicy(
        GlobalConstants.CommentCreatePolicy, policy => policy.RequireClaim("permission", GlobalConstants.CommentCreatePermission));
    options.AddPolicy(
        GlobalConstants.CommentDeletePolicy, policy => policy.RequireClaim("permission", GlobalConstants.CommentDeletePermission));
    options.AddPolicy(
        GlobalConstants.CommentGetPolicy, policy => policy.RequireClaim("permission", GlobalConstants.CommentGetPermission));
    options.AddPolicy(
        GlobalConstants.GameCreatePolicy, policy => policy.RequireClaim("permission", GlobalConstants.GameCreatePermission));
    options.AddPolicy(
        GlobalConstants.GameUpdatePolicy, policy => policy.RequireClaim("permission", GlobalConstants.GameUpdatePermission));
    options.AddPolicy(
        GlobalConstants.GameDeletePolicy, policy => policy.RequireClaim("permission", GlobalConstants.GameDeletePermission));
    options.AddPolicy(
        GlobalConstants.GameGetPolicy, policy => policy.RequireClaim("permission", GlobalConstants.GameGetPermission));
    options.AddPolicy(
        GlobalConstants.GenreCreatePolicy, policy => policy.RequireClaim("permission", GlobalConstants.GenreCreatePermission));
    options.AddPolicy(
        GlobalConstants.GenreUpdatePolicy, policy => policy.RequireClaim("permission", GlobalConstants.GenreUpdatePermission));
    options.AddPolicy(
        GlobalConstants.GenreDeletePolicy, policy => policy.RequireClaim("permission", GlobalConstants.GenreDeletePermission));
    options.AddPolicy(
        GlobalConstants.GenreGetPolicy, policy => policy.RequireClaim("permission", GlobalConstants.GenreGetPermission));
    options.AddPolicy(
        GlobalConstants.PlatformTypeCreatePolicy, policy => policy.RequireClaim("permission", GlobalConstants.PlatformTypeCreatePermission));
    options.AddPolicy(
        GlobalConstants.PlatformTypeUpdatePolicy, policy => policy.RequireClaim("permission", GlobalConstants.PlatformTypeUpdatePermission));
    options.AddPolicy(
        GlobalConstants.PlatformTypeDeletePolicy, policy => policy.RequireClaim("permission", GlobalConstants.PlatformTypeDeletePermission));
    options.AddPolicy(
        GlobalConstants.PlatformTypeGetPolicy, policy => policy.RequireClaim("permission", GlobalConstants.PlatformTypeGetPermission));
    options.AddPolicy(
        GlobalConstants.PublisherCreatePolicy, policy => policy.RequireClaim("permission", GlobalConstants.PublisherCreatePermission));
    options.AddPolicy(
        GlobalConstants.PublisherUpdatePolicy, policy => policy.RequireClaim("permission", GlobalConstants.PublisherUpdatePermission));
    options.AddPolicy(
        GlobalConstants.PublisherDeletePolicy, policy => policy.RequireClaim("permission", GlobalConstants.PublisherDeletePermission));
    options.AddPolicy(
        GlobalConstants.PublisherGetPolicy, policy => policy.RequireClaim("permission", GlobalConstants.PublisherGetPermission));
    options.AddPolicy(
        GlobalConstants.RoleCreatePolicy, policy => policy.RequireClaim("permission", GlobalConstants.RoleCreatePermission));
    options.AddPolicy(
        GlobalConstants.RoleUpdatePolicy, policy => policy.RequireClaim("permission", GlobalConstants.RoleUpdatePermission));
    options.AddPolicy(
        GlobalConstants.RoleDeletePolicy, policy => policy.RequireClaim("permission", GlobalConstants.RoleDeletePermission));
    options.AddPolicy(
        GlobalConstants.RoleGetPolicy, policy => policy.RequireClaim("permission", GlobalConstants.RoleGetPermission));
    options.AddPolicy(
        GlobalConstants.RolePermissionGetPolicy, policy => policy.RequireClaim("permission", GlobalConstants.RolePermissionGetPermission));
    options.AddPolicy(
        GlobalConstants.UserCreatePolicy, policy => policy.RequireClaim("permission", GlobalConstants.UserCreatePermission));
    options.AddPolicy(
        GlobalConstants.UserUpdatePolicy, policy => policy.RequireClaim("permission", GlobalConstants.UserUpdatePermission));
    options.AddPolicy(
        GlobalConstants.UserDeletePolicy, policy => policy.RequireClaim("permission", GlobalConstants.UserDeletePermission));
    options.AddPolicy(
        GlobalConstants.UserGetPolicy, policy => policy.RequireClaim("permission", GlobalConstants.UserGetPermission));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

IdentityModelEventSource.ShowPII = true;
app.UseMiddleware<CustomErrorLoggingMiddleware>();
app.UseMiddleware<CustomLoggingMiddleware>();
app.UseMiddleware<CustomLoggingIpAddressMiddleware>();
app.UseMiddleware<CustomExceptionHandlerMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<GameStoreDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception)
    {
        throw;
    }
}

app.UseSwagger();
app.UseSwaggerUI(config =>
{
    config.RoutePrefix = string.Empty;
    config.SwaggerEndpoint("swagger/v1/swagger.json", "GameStore");
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
