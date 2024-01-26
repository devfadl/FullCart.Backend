using AutoMapper;

using FullCart.Application.Common.Dto;
using FullCart.Application.Common.Interfaces;
using FullCart.Application.Common.Mappings;
using FullCart.Application.Common.Shared;
using FullCart.Domain.ValueObjects;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace FullCart.Application.User.Queries.GetUsersById;

public record GetUsersByIdQuery : IRequest<Result<UserBriefDto>>
{
    public Guid Id { get; init; }
}

public class GetUsersByIdQueryHandler : IRequestHandler<GetUsersByIdQuery, Result<UserBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUsersByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<UserBriefDto>> Handle(GetUsersByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty)
        {
            return Result<UserBriefDto>.NotFound(Localization.ERROR_NOT_FOUND);
        }


        var user = await _context.Users
            .Where(x => x.Id == request.Id)
            .ProjectToQueryAsync<UserBriefDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
            return Result<UserBriefDto>.NotFound(Localization.ERROR_NOT_FOUND);

        return Result<UserBriefDto>.Success(user);
    }
}