using AutoMapper;

using FullCart.Application.Common.Dto.Orders;
using FullCart.Application.Common.Interfaces;
using FullCart.Application.Common.Mappings;
using FullCart.Application.Common.Shared;
using FullCart.Domain.ValueObjects;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace FullCart.Application.Features.Orders.Queries;

public record GetOrderByIdQuery(Guid Id) : IRequest<Result<OrderBrief>>;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Result<OrderBrief>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetOrderByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<OrderBrief>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var Order = await _context.Order
                .Include(r => r.Customer).ThenInclude(a=>a.User)
                .Include(s => s.OrderStatus)
                .Include(a=> a.OrderDetails).ThenInclude(a=>a.Product)
                .Where(a=>a.Id == request.Id)
                .ProjectToQueryAsync<OrderBrief>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

        if (Order == null)
            return Result<OrderBrief>.NotFound(Localization.ERROR_NOT_FOUND);

        return Result<OrderBrief>.Success(Order);

    }
}
