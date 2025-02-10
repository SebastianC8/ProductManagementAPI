using Application.DTO;
using AutoMapper;
using Core.Entities;
using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductCore, Product>().ReverseMap();
            CreateMap<ProductCore, CreateProductDTO>().ReverseMap();
            CreateMap<ProductCore, UpdateProductDTO>().ReverseMap();
            CreateMap<ProductCore, ProductResponseDTO>().ReverseMap();
        }
    }
}
