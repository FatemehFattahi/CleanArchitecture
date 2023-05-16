using CleanArchitecture.Application.Dtos;

namespace CleanArchitecture.Application.Services;

public interface ISellerService
{
    Task<List<SellerDto>> GetAllAsync(CancellationToken cancellationToken);
}