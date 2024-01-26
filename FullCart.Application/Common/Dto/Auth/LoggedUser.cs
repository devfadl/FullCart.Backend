using Microsoft.AspNetCore.Http;

using System.Security.Claims;

namespace FullCart.Application.Common.Dto;

public class LoggedUser
{
    private readonly IHttpContextAccessor _httpContext;

    public LoggedUser(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }


    public Guid Id
    {
        get
        {
            var uuid = _httpContext.HttpContext.User.Claims.FirstOrDefault(p => p.Type == "UserId")?.Value;
            return Guid.Parse(uuid);
        }
    }

    public List<int> Roles
    {
        get
        {
            return _httpContext.HttpContext.User.Claims.Where(p => p.Type.Equals(ClaimTypes.Role)).Select(p => int.Parse(p.Value)).ToList();
        }
    }

    public string FullName
    {
        get
        {
            return _httpContext.HttpContext.User.Claims.FirstOrDefault(p => p.Type == "Full Name")?.Value;
        }
    }
}
