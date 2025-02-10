using AutoMapper;
using Core.Contracts;
using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductCore>
    {
        private IProductCore __productCore;
        private IMapper __mapper;

        public GetProductByIdQueryHandler(IProductCore productCore,IMapper mapper)
        {
            __productCore = productCore;
            __mapper = mapper;
        }

        public async Task<ProductCore> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await __productCore.GetById(request.id);
            var productCore = __mapper.Map<ProductCore>(product);
            return productCore;
        }
    }
}
