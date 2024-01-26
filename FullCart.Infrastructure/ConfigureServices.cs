using FullCart.Application.Common.Interfaces;
using FullCart.Application.Common.Security;
using FullCart.Application.Common.Services;
using FullCart.Application.Common.Shared;
using FullCart.Infrastructure.Persistence;
using FullCart.Infrastructure.Persistence.Interceptors;
using FullCart.Infrastructure.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Serilog;

namespace Microsoft.Extensions.DependencyInjection;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration, AppSetting appSetting)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddSingleton<IConverterService, ConverterService>();
        services.AddScoped<IUserTransactionService, UserTransactionService>();
        services.AddScoped<ITokenService, TokenService>();
 
        services.AddSingleton(Log.Logger);
        return services;
    }
}
