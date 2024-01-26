using AutoMapper;

using FullCart.Application.Common.Dto;
using FullCart.Application.Common.Interfaces;
using FullCart.Application.Common.Mappings;
using FullCart.Domain.Enums;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace FullCart.Application.Permission.Queries.GetPermissionsWithPagination;

public record GetPermissionsQuery : IRequest<List<PermissionBriefDto>>
{
}

public class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, List<PermissionBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPermissionsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PermissionBriefDto>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Permissions
            .Where(p => p.Id == (int)PermissionEnum.UsersAndGroupsAdministration)
            .OrderBy(x => x.Name)
            .ProjectToQueryAsync<PermissionBriefDto>(_mapper.ConfigurationProvider).ToListAsync();
    }
}
