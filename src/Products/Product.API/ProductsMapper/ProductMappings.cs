using AutoMapper;
using Product.API.Models;
using Product.API.Models.Dtos;

namespace Product.API.ProductsMapper
{
    public class ProductMappings : Profile
    {
        public ProductMappings()
        {
            CreateMap<Pie, PieDto>().ReverseMap();
            CreateMap<PieType, PieTypeDto>().ReverseMap();

        }

    }
}
