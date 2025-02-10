using Application.DTO;
using FluentValidation;

namespace Application.Validators
{
    public class ProductUpdateValidator : AbstractValidator<UpdateProductDTO>
    {
        public ProductUpdateValidator()
        {
            RuleFor(r => r.Id).GreaterThan(0).WithMessage("Product {PropertyName} must be greather than 0");
            RuleFor(r => r.Name).NotEmpty().WithMessage("Product {PropertyName} can't be empty");
            RuleFor(r => r.Name).MinimumLength(5).WithMessage(m => "The length of {PropertyName} must be greather than 5.");
            RuleFor(r => r.Quantity).GreaterThan(0).WithMessage("Product {PropertyName} must be greather than 0");
        }
    }
}
