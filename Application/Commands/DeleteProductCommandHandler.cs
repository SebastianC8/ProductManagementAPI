using Application.DTO;
using AutoMapper;
using Core.Contracts;
using Core.Entities;
using MediatR;

namespace Application.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ProductCore>
    {
        private IProductCore __productCore;
        private IMapper __mapper;

        public DeleteProductCommandHandler(IProductCore productCore, IMapper mapper)
        {
            __productCore = productCore;
            __mapper = mapper;
        }

        public async Task<ProductCore> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productCore = await __productCore.DeleteById(request.id);
            return productCore;
        }
    }
}
