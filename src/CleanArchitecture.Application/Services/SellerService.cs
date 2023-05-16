using AutoMapper;
using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Repositories;

namespace CleanArchitecture.Application.Services;

public class SellerService : ISellerService
{
    private readonly ISellerRepository _sellerRepository;
    private readonly IMapper _mapper;

    public SellerService(ISellerRepository sellerRepository, IMapper mapper)
    {
        _sellerRepository = sellerRepository;
        _mapper = mapper;
    }

    public async Task<List<SellerDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var sellers = await _sellerRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<List<SellerDto>>(sellers);
    }
}