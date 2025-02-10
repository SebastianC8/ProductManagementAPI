using Application.DTO;
using MediatR;

namespace Application.Commands
{
    public record UpdateProductCommand(UpdateProductDTO updateProductDTO): IRequest<ProductResponseDTO>;
}
