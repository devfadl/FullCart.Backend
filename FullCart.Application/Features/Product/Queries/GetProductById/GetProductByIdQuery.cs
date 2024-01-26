using AutoMapper;

using FullCart.Application.Common.Dto.Products;
using FullCart.Application.Common.Interfaces;
using FullCart.Application.Common.Mappings;
using FullCart.Application.Common.Shared;
using FullCart.Domain.ValueObjects;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace FullCart.Application.Features.Products.Queries;

public record GetProductByIdQuery(Guid Id) : IRequest<Result<ProductBrief>>;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductBrief>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<ProductBrief>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Product
                .Include(r => r.Brand)
                .Include(s => s.ProductCategory)
                .Include(s => s.ProductImages)
                .Where(a=>a.Id == request.Id)
                .ProjectToQueryAsync<ProductBrief>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

        if (product == null)
            return Result<ProductBrief>.NotFound(Localization.ERROR_NOT_FOUND);
        return Result<ProductBrief>.Success(product);

    }
}
