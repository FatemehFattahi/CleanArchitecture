using AutoMapper;
using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Domain.Models;

namespace CleanArchitecture.Application.MappingProfiles;

public class ProductPriceMappingProfile : Profile
{
    public ProductPriceMappingProfile()
    {
        CreateMap<AddProductPriceRequestDto, ProductPrice>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Product, opt => opt.Ignore())
            .ForMember(dest => dest.Seller, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        CreateMap<ProductPrice, ProductPriceDto>()
            .ReverseMap();
    }
}