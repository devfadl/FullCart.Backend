using Api.Controllers;

using FullCart.Application.Common.Dto;
using FullCart.Application.Permission.Queries.GetPermissionsWithPagination;
using FullCart.Domain.ValueObjects;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullCart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permissions.Admininistrator)]
    public class PermissionController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<PermissionBriefDto>>> GetPermissionsQuery([FromQuery] GetPermissionsQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
