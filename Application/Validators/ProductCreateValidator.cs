using Application.DTO;
using FluentValidation;

namespace Application.Validators
{
    public class ProductCreateValidator: AbstractValidator<CreateProductDTO>
    {
        public ProductCreateValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage("Product's name can't be empty");
            RuleFor(r => r.Name).MinimumLength(5).WithMessage(m => "The length of {PropertyName} must be greather than 5.");
            RuleFor(r => r.Quantity).GreaterThan(0).WithMessage("Product's quantity must be greather than 0");
        }
    }
}
