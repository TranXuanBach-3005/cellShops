using AutoMapper;
using cellShopSloution.ViewModel.Dtos.ProductImage;
using cellShopSolution.Data.Entities;
using cellShopSolution.ViewModel.Dtos.Products;

namespace cellShopSolution.ViewModel.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ProductCreateRequest, Product>().ReverseMap();
            CreateMap<ProductCreateRequest, ProductTranslation>().ReverseMap();
            CreateMap<Product, ProductUpdateRequest>().ReverseMap();
            CreateMap<ProductImageCreateRequest,ProductImage >().ReverseMap();
        }
    }
}
