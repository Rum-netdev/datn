using AutoMapper;
using EcommercialWebApp.Data.Models;
using EcommercialWebApp.Handler.Products.Dtos;

namespace EcommercialWebApp.Handler.Mapper.Profiles
{
    public class ProductProfile : Profile, IBaseProfileTransient
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductViewModel>();
        }
    }
}
