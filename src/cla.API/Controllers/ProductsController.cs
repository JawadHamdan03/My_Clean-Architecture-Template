using cla.Application.Features.Products.CreateProduct;
using cla.Application.Features.Products.GetProducts;
using cla.Application.Features.Products.Requests;
using cla.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cla.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles ="Customer")]
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> addProduct(CreateProductRequest request)
    {
        var res = await mediator.Send(new CreateProductCommand(request));

        return Ok(res);
    } 

    [HttpGet]
    public async Task<IActionResult> getAll()
    {
        var res = await mediator.Send(new GetProductsQuery());
        return Ok(res);
    }
}