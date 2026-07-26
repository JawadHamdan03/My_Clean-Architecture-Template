using cla.Application.Common;
using cla.Application.Features.Products.Responses;
using cla.Domain.Entities;
using Mapster;
using MediatR;

namespace cla.Application.Features.Products.CreateProduct;



public class CreateProductCommandHandler(IAppDbContext dbContext) : IRequestHandler<CreateProductCommand, CreateProductResponse>
{
    public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var prod = request.CreateProduct.Adapt<Product>();

        await dbContext.Products.AddAsync(prod);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreateProductResponse(){Name=prod.Name,Price=prod.Price};
    }
}