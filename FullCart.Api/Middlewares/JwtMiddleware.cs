using FullCart.Application.Common.Interfaces;

using Microsoft.AspNetCore.Authorization;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FullCart.Api.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate next;
    private readonly IServiceProvider provider;

    public JwtMiddleware(RequestDelegate next, IServiceProvider provider)
    {
        this.next = next;
        this.provider = provider;
    }
    public async Task Invoke(HttpContext context)
    {
        bool allowAnonymous = context.GetEndpoint()?.Metadata?.GetMetadata<IAllowAnonymous>() != null;

        // If the action or controller is marked as [AllowAnonymous], skip token validation.
        if (allowAnonymous)
        {
            await next(context);
            return;
        }
        // Get the authorization header from the request.
        string authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();

        // If the header is missing or not of the correct format, return a 401 unauthorized response.
        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }
        // Extract the token from the header.
        string token = authorizationHeader.Substring("Bearer ".Length).Trim();

        var success = await ValidateToken(context, token);
        if (!success)
        {
            context.Response.StatusCode = 401;
            return;
        }

        await this.next(context);
    }

    private async Task<bool> ValidateToken(HttpContext context, string token)
    {

        using var serviceScope = provider.CreateScope();

        var services = serviceScope.ServiceProvider;
        var userAuthServices = services.GetRequiredService<IUserAuthServices>();
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
        var userName = jwt.Claims.First(c => c.Type == "userName").Value;
        var user = await userAuthServices.GetUser(userName);

        if (user == null)
            return false;

        var userPermissions = await userAuthServices.GetUserPermissions(user.Id);

        var tokenClaims = context.User.Claims.Where(x => x.Type == ClaimTypes.Role).Select(a => a.Value).ToList();

        if (!userPermissions.All(tokenClaims.Contains))
            return false;
        return true;
    }
}
public static class JwtMiddlewareExtensions
{
    public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<JwtMiddleware>();
    }
}