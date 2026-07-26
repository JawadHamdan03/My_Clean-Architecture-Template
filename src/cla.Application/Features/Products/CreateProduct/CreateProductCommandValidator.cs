using FluentValidation;

namespace cla.Application.Features.Products.CreateProduct;


public class CreateProductCommandValidator: AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x=> x.CreateProduct.Name).NotEmpty().WithMessage("Product can't have an empty name");
        RuleFor(x=> x.CreateProduct.Price).GreaterThan(0).WithMessage("Product Price must have a positive value");
    }
}