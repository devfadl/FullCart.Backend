using AutoMapper;

using FullCart.Application.Common.Dto;
using FullCart.Application.Common.Interfaces;
using FullCart.Application.Common.Shared;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace FullCart.Application.User.Queries.GetUsersStatistics;

public record GetUsersStatisticsQuery : IRequest<Result<UserStatisticsBriefDto>>
{
}

public class GetUsersStatisticsQueryHandler : IRequestHandler<GetUsersStatisticsQuery, Result<UserStatisticsBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUsersStatisticsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<UserStatisticsBriefDto>> Handle(GetUsersStatisticsQuery request, CancellationToken cancellationToken)
    {
        var inActiveUsersCount = await _context.Users.CountAsync(p => !p.IsActive);
        var activeUsersCount = await _context.Users.CountAsync(p => p.IsActive);
        var totalUsersCount = inActiveUsersCount + activeUsersCount;

        var user = new UserStatisticsBriefDto
        {
            ActiveUsers = activeUsersCount,
            InActiveUsers = inActiveUsersCount,
            TotalUsers = totalUsersCount,
        };

        return Result<UserStatisticsBriefDto>.Success(user);
    }
}