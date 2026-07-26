using cla.Application.Features.Products.Responses;
using MediatR;

namespace cla.Application.Features.Products.GetProducts;


public sealed record GetProductsQuery: IRequest<List<CreateProductResponse>>;