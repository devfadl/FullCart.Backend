using AutoMapper;
using AutoMapper.QueryableExtensions;

using FullCart.Application.Common.Dto;
using FullCart.Application.Common.Interfaces;
using FullCart.Application.Common.Mappings;
using FullCart.Application.Common.Shared;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace FullCart.Application.User.Queries.GetUsersWithPagination;

public record GetUsersWithPaginationQuery : IRequest<PaginatedList<UserBrief>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public bool? ActivationStatus { get; init; } = null;
    public string SearchText { get; init; } = String.Empty;
}

public class GetUsersWithPaginationQueryHandler : IRequestHandler<GetUsersWithPaginationQuery, PaginatedList<UserBrief>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUsersWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<UserBrief>> Handle(GetUsersWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Users.Include(p => p.UserGroups).AsQueryable();

        if (request.ActivationStatus.HasValue)
        {
            query = query.Where(p => p.IsActive == request.ActivationStatus);
        }

        if (!string.IsNullOrEmpty(request.SearchText))
        {
            var searchFor = request.SearchText.Trim();
            query = query.Where(p => p.FirstName.Contains(searchFor) || p.SecondName.Contains(searchFor) || p.ThirdName.Contains(searchFor) ||
            p.LastName.Contains(searchFor) || p.Username.Contains(searchFor) || p.Email.Contains(searchFor));
        }

        return await query.OrderBy(x => x.FirstName)
            .ProjectTo<UserBrief>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}