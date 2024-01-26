using Api.Controllers;

using FullCart.Application.Common.Shared;
using FullCart.Application.UserGroups.Commands.AddUserGroup;
using FullCart.Domain.ValueObjects;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullCart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permissions.Admininistrator)]
    public class UserGroupController : ApiControllerBase
    {
        [HttpPost()]
        public async Task<Result<bool>> AddUserGroup(AddUserGroupCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
