using FluentValidation;

using FullCart.Application.Common.Behaviours;
using FullCart.Application.Common.Interfaces;
using FullCart.Application.Common.Services;

using MediatR;

using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddScoped<IUserAuthServices, UserAuthServices>();
        services.AddScoped<FullCart.Application.Common.Dto.LoggedUser>();

        return services;
    }
}
