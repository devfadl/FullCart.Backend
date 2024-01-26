using Api.Controllers;

using FullCart.Application.Common.Shared;
using FullCart.Application.Group.Commands.CreateGroup;
using FullCart.Domain.ValueObjects;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullCart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permissions.Admininistrator)]
    public class GroupPermissionController : ApiControllerBase
    {
        [HttpPost()]
        public async Task<Result<bool>> AddGroupPermission(AddGroupPermissionCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
