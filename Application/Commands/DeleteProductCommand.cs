using Application.DTO;
using Core.Entities;
using MediatR;

namespace Application.Commands
{
    public record DeleteProductCommand(int id): IRequest<ProductCore>;
}
