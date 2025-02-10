
using Application.DTO;
using AutoMapper;
using Core.Contracts;
using Core.Entities;
using MediatR;

namespace Application.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponseDTO>
    {

        private IProductCore _productCore;
        private IMapper _mapper;

        public CreateProductCommandHandler(IProductCore productCore, IMapper mapper)
        {
            _productCore = productCore;
            _mapper = mapper;
        }

        public async Task<ProductResponseDTO> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var newProductCore = _mapper.Map<ProductCore>(request.createProductDTO);
            var productCreated = await _productCore.Add(newProductCore);
            var productResponseDto = _mapper.Map<ProductResponseDTO>(productCreated);

            return productResponseDto;
        }
    }
}
