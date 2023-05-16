using AutoMapper;
using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Domain.Models;

namespace CleanArchitecture.Application.MappingProfiles;

public class SellerMappingProfile : Profile
{
    public SellerMappingProfile()
    {
        CreateMap<Seller, SellerDto>();
    }
}