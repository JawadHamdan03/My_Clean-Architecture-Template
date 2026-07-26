using cla.Application.Common;
using cla.Application.Features.Products.Responses;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace cla.Application.Features.Products.GetProducts;


public class GetProductsQueryHandler(IAppDbContext dbContext) : IRequestHandler<GetProductsQuery, List<CreateProductResponse>>
{
    public async Task<List<CreateProductResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var res =await (dbContext.Products.ToListAsync());
        return res.Adapt<List<CreateProductResponse>>();

    }
}