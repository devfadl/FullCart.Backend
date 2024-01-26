using FullCart.Api.Filters;
using FullCart.Application.Common.Shared;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using System.Text;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, AppSetting _appSetting)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddHttpContextAccessor();
        services.AddControllersWithViews(options =>
        {
            options.Filters.Add<ApiExceptionFilterAttribute>();
            options.Filters.Add<ApiActionFilter>();
        });

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     options.SaveToken = true;
                     options.RequireHttpsMetadata = false;
                     options.TokenValidationParameters = new TokenValidationParameters()
                     {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidAudience = _appSetting.APISecurity.Audience,
                         ValidIssuer = _appSetting.APISecurity.Issuer,
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSetting.APISecurity.Key)),
                     };
                 });

        services.AddAuthorization(options =>
        {
            // By default, all incoming requests will be authorized according to the default policy.
            options.FallbackPolicy = options.DefaultPolicy;
        });
        return services;
    }
}
