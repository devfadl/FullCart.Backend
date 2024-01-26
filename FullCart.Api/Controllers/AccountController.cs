using Api.Controllers;

using FullCart.Application.Auth.Login;
using FullCart.Application.Common.Dto;
using FullCart.Application.Common.Shared;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullCart.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AccountController : ApiControllerBase
{
    private readonly ILogger<LoginCommand> logger;

    public AccountController(ILogger<LoginCommand> logger)
    {
        this.logger = logger;
    }
    [AllowAnonymous]
    [HttpPost]
    public async Task<Result<AuthBrief>> Login(LoginCommand command)
    {
        return await Mediator.Send(command);
    }
}
