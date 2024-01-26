using Api.Controllers;

using FullCart.Application.Common.Dto;
using FullCart.Application.Common.Shared;
using FullCart.Application.Group.Commands.CreateGroup;
using FullCart.Application.Group.Commands.UpdateGroup;
using FullCart.Application.Group.Queries.GetActiveGroups;
using FullCart.Application.Group.Queries.GetGroupById;
using FullCart.Application.Group.Queries.GetGroupsWithPagination;
using FullCart.Domain.ValueObjects;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullCart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permissions.Admininistrator)]

    public class GroupsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<GroupBriefDto>>> GetGroupsWithPaginationQuery([FromQuery] GetGroupsWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet]
        [Route("active-groups")]
        public async Task<ActionResult<List<GroupBriefDto>>> GetActiveGroups([FromQuery] GetActiveGroupsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet()]
        [Route("{Id}")]
        public async Task<Result<GroupBriefDto>> GetGroupByIdQuery([FromQuery] GetGroupByIdQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost()]
        public async Task<Result<Guid>> CreateGroup(CreateGroupCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete()]
        public async Task<Result<bool>> DeleteGroup(DeleteGroupCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost()]
        [Route("edit")]
        public async Task<Result<bool>> UpdateGroup(UpdateGroupCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
