using AutoMapper;

using FullCart.Application.Common.Dto.Orders;
using FullCart.Application.Common.Interfaces;
using FullCart.Application.Common.Mappings;
using FullCart.Application.Common.Shared;
using FullCart.Domain.Enums;

using MediatR;

using Microsoft.EntityFrameworkCore;

using System.Linq;

namespace FullCart.Application.Features.Orders.Queries;

public record GetOrdersPaginatedQuery(string? OrderNumber, OrderStatusEnum? Status,string? FromDate, string? ToDate, int PageNumber = 1, int PageSize = 10) : IRequest<Result<PaginatedList<OrdersBrief>>>;

public class GetOrdersPaginatedQueryHandler : IRequestHandler<GetOrdersPaginatedQuery, Result<PaginatedList<OrdersBrief>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetOrdersPaginatedQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<OrdersBrief>>> Handle(GetOrdersPaginatedQuery request, CancellationToken cancellationToken)
    {
        DateTime? fromdate = !string.IsNullOrWhiteSpace(request.FromDate) ? DateTime.Parse(request.FromDate) : null;
        DateTime? toDate = !string.IsNullOrWhiteSpace(request.ToDate) ? DateTime.Parse(request.ToDate) : null;
        var query = _context.Order
                .Include(r => r.Customer).ThenInclude(a=>a.User)
                .Include(s => s.OrderStatus)
                .WhereIf(request.Status.HasValue, x => x.OrderStatusId == (int)request.Status)
                .WhereIf(!string.IsNullOrWhiteSpace(request.OrderNumber), x => x.OrderNumber.Contains(request.OrderNumber))
                .WhereIf(fromdate != null, x => x.OrderDate.Date >= fromdate.Value.Date)
                .WhereIf(toDate != null, x => x.OrderDate.Date >= toDate.Value.Date)
                .OrderByDescending(a => a.Created).AsQueryable();
        var OrdersCount = await query.CountAsync(cancellationToken: cancellationToken);
        var Orders = await query.Pagination(request.PageNumber, request.PageSize).ToListAsync();
        var result = new PaginatedList<OrdersBrief>(_mapper.Map<List<OrdersBrief>>(Orders), OrdersCount, request.PageNumber, request.PageSize);
        return Result<PaginatedList<OrdersBrief>>.Success(result);

    }
}
