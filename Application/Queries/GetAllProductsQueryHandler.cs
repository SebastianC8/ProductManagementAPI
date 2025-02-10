using Application.DTO;
using AutoMapper;
using Core.Contracts;
using MediatR;

namespace Application.Queries
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductResponseDTO>>
    {
        private IProductCore __productCore;
        private IMapper __mapper;

        public GetAllProductsQueryHandler(IProductCore productCore, IMapper mapper)
        {
            __productCore = productCore;
            __mapper = mapper;
        }

        public async Task<IEnumerable<ProductResponseDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            // Obtiene todos los productos desde la capa Core
            var productCoreList = await __productCore.GetAll();

            // Mapea la lista de ProductCore a una lista de ProductResponseDTO
            var productResponseList = __mapper.Map<IEnumerable<ProductResponseDTO>>(productCoreList);

            return productResponseList;
        }
    }
}
