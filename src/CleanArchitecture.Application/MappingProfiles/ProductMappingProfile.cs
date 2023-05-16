using AutoMapper;
using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Domain.Models;

namespace CleanArchitecture.Application.MappingProfiles;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductDto>()
            .ReverseMap();

        CreateMap<AddProductRequestDto, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ProductPrices, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .DisableCtorValidation();

    }
}