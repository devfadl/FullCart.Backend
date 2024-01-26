using AutoMapper;

using FullCart.Application.Common.Dto;
using FullCart.Application.Common.Interfaces;
using FullCart.Application.Common.Mappings;
using FullCart.Application.Common.Shared;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace FullCart.Application.Group.Queries.GetGroupsWithPagination;

public record GetGroupsWithPaginationQuery : IRequest<PaginatedList<GroupBriefDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;

}
public class GetGroupsWithPaginationQueryHandler : IRequestHandler<GetGroupsWithPaginationQuery, PaginatedList<GroupBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetGroupsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<GroupBriefDto>> Handle(GetGroupsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var groups = await _context.Groups
                 .Include(p => p.GroupPermissions)
                 .OrderBy(x => x.Name)
                 .ProjectToQueryAsync<GroupBriefDto>(_mapper.ConfigurationProvider)
                 .PaginatedListAsync(request.PageNumber, request.PageSize);

        var groupIds = groups.Items.Select(p => p.Id).ToList();

        var groupUserCount = _context.Groups.Where(p => groupIds.Contains(p.Id))
              .Select(p => new
              {
                  GroupId = p.Id,
                  UsersCount = p.UserGroups.Count()
              }).ToArray();

        foreach (var group in groups.Items)
        {
            group.UsersCount = groupUserCount.Where(p => p.GroupId == group.Id).Select(p => p.UsersCount).FirstOrDefault();
        }

        return groups;
    }
}
