using AutoMapper;

using FullCart.Application.Common.Dto.Products;
using FullCart.Application.Common.Interfaces;
using FullCart.Application.Common.Mappings;
using FullCart.Application.Common.Shared;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace FullCart.Application.Features.Products.Queries;

public record GetProductsPaginatedQuery(string? Name, string? SKU, int PageNumber = 1, int PageSize = 10) : IRequest<Result<PaginatedList<ProductsBrief>>>;

public class GetProductsPaginatedQueryHandler : IRequestHandler<GetProductsPaginatedQuery, Result<PaginatedList<ProductsBrief>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductsPaginatedQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<ProductsBrief>>> Handle(GetProductsPaginatedQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Product
                .Include(r => r.Brand)
                .Include(s => s.ProductCategory)
                .WhereIf(!string.IsNullOrWhiteSpace(request.SKU), x => x.SKU == request.SKU)
                .WhereIf(!string.IsNullOrWhiteSpace(request.Name), x => x.Name.Contains(request.Name))
                .OrderByDescending(a => a.Created).AsQueryable();
        var productsCount = await query.CountAsync(cancellationToken: cancellationToken);
        var products = await query.Pagination(request.PageNumber, request.PageSize).ToListAsync();
        var result = new PaginatedList<ProductsBrief>(_mapper.Map<List<ProductsBrief>>(products), productsCount, request.PageNumber, request.PageSize);
        return Result<PaginatedList<ProductsBrief>>.Success(result);

    }
}
