using AutoMapper;

using FullCart.Application.Common.Dto;
using FullCart.Application.Common.Interfaces;
using FullCart.Application.Common.Mappings;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace FullCart.Application.Group.Queries.GetActiveGroups;

public record GetActiveGroupsQuery : IRequest<List<GroupBriefDto>>
{
}
public class GetGetActiveGroupsQuerynQueryHandler : IRequestHandler<GetActiveGroupsQuery, List<GroupBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetGetActiveGroupsQuerynQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GroupBriefDto>> Handle(GetActiveGroupsQuery request, CancellationToken cancellationToken)
    {
        var groups = await _context.Groups
                 .Where(p => p.IsActive)
                 .OrderBy(x => x.Name)
                 .ProjectToQueryAsync<GroupBriefDto>(_mapper.ConfigurationProvider)
                 .ToListAsync();

        return groups;
    }
}
