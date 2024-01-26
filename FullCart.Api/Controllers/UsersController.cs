using Api.Controllers;

using FullCart.Application.Common.Dto;
using FullCart.Application.Common.Shared;
using FullCart.Application.User.Commands.CreateUser;
using FullCart.Application.User.Commands.UpdateUser;
using FullCart.Application.User.Queries.GetAllUsers;
using FullCart.Application.User.Queries.GetUsersStatistics;
using FullCart.Application.User.Queries.GetUsersWithPagination;
using FullCart.Domain.ValueObjects;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullCart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permissions.Admininistrator)]
    public class UsersController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<UserBrief>>> GetUsersWithPaginationQuery([FromQuery] GetUsersWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet]
        [Route("all-users")]
        public async Task<ActionResult<List<UserBrief>>> GetAllUsers([FromQuery] GetAllUsersQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet]
        [Route("user-statistics")]
        public async Task<Result<UserStatisticsBriefDto>> GetUsersStatisticsQuery([FromQuery] GetUsersStatisticsQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost()]
        public async Task<Result<Guid>> CreateUser(CreateUserCommand command)
        {
            return await Mediator.Send(command);

        }

        [HttpPost()]
        [Route("edit")]
        public async Task<Result<bool>> UpdateUser(UpdateUserCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}