using WhatsBack.Application.Features.Products.Queries.GetAllProducts;
using AutoMapper;
using WhatsBack.Domain.Entities;
using WhatsBack.Application.UseCases.Products.Commands.CreateProduct;

namespace WhatsBack.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Product, GetAllProductsViewModel>().ReverseMap();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<GetAllProductsQuery, GetAllProductsParameter>();
        }
    }
}
