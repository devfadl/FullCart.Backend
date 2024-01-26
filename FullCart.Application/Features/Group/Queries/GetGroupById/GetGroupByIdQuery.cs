using AutoMapper;

using FullCart.Application.Common.Dto;
using FullCart.Application.Common.Interfaces;
using FullCart.Application.Common.Mappings;
using FullCart.Application.Common.Shared;
using FullCart.Domain.ValueObjects;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace FullCart.Application.Group.Queries.GetGroupById;

public record GetGroupByIdQuery : IRequest<Result<GroupBriefDto>>
{
    public Guid Id { get; init; }
}

public class GetGroupByIdQueryHandler : IRequestHandler<GetGroupByIdQuery, Result<GroupBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetGroupByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<GroupBriefDto>> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty)
        {
            return Result<GroupBriefDto>.NotFound(Localization.ERROR_NOT_FOUND);
        }


        var group = await _context.Groups
            .Where(x => x.Id == request.Id)
            .ProjectToQueryAsync<GroupBriefDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (group == null)
            return Result<GroupBriefDto>.NotFound(Localization.ERROR_NOT_FOUND);

        return Result<GroupBriefDto>.Success(group);
    }
}