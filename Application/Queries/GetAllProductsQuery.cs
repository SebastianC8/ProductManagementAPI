using Application.DTO;
using MediatR;

namespace Application.Queries
{
    public record GetAllProductsQuery(): IRequest<IEnumerable<ProductResponseDTO>>;
}
