using AutoMapper;

using FullCart.Application.Common.Dto;
using FullCart.Application.Common.Interfaces;
using FullCart.Application.Common.Mappings;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace FullCart.Application.User.Queries.GetAllUsers;

public record GetAllUsersQuery : IRequest<List<UserBrief>>
{
}

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserBrief>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<UserBrief>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _context.Users
           .AsNoTracking()
           .ProjectToQueryAsync<UserBrief>(_mapper.ConfigurationProvider)
           .ToListAsync();

        return users;
    }
}