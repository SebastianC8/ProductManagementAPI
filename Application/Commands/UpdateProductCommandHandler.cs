using Application.DTO;
using Application.Notifications;
using AutoMapper;
using Core.Contracts;
using Core.Entities;
using MediatR;

namespace Application.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductResponseDTO>
    {
        private IProductCore __productCore;
        private IMapper __mapper;
        private IMediator __mediator;

        public UpdateProductCommandHandler(IProductCore productCore, IMapper mapper, IMediator mediator)
        {
            __productCore = productCore;
            __mapper = mapper;
            __mediator = mediator;
        }

        public async Task<ProductResponseDTO> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var updatedProductCore = __mapper.Map<ProductCore>(request.updateProductDTO);
            var updatedProduct = await __productCore.Update(updatedProductCore);

            if (updatedProduct.Quantity <= 5)
            {
                await __mediator.Publish(new LowStockNotification(updatedProduct), cancellationToken);
            }

            return __mapper.Map<ProductResponseDTO>(updatedProduct);
        }
    }
}
