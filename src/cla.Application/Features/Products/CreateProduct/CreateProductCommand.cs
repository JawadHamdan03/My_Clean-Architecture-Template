using cla.Application.Features.Products.Requests;
using cla.Application.Features.Products.Responses;
using cla.Domain.Entities;
using MediatR;

namespace cla.Application.Features.Products.CreateProduct;


public sealed record CreateProductCommand(CreateProductRequest CreateProduct) : IRequest<CreateProductResponse>;