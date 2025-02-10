using Application.DTO;
using MediatR;

namespace Application.Commands
{
    public record CreateProductCommand(CreateProductDTO createProductDTO): IRequest<ProductResponseDTO>;
}
