using AutoMapper;
using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Repositories;
using CleanArchitecture.Domain.Models;

namespace CleanArchitecture.Application.Services
{
    public class ProductPriceService : IProductPriceService
    {
        private readonly IProductPriceRepository _productPriceRepository;
        private readonly ISellerRepository _sellerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductPriceService(IProductPriceRepository productPriceRepository,
            ISellerRepository sellerRepository, IProductRepository productRepository, IMapper mapper)
        {
            _productPriceRepository = productPriceRepository;
            _sellerRepository = sellerRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductPriceDto>> GetAllPricesForProductAsync(int productId,
            CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(productId, cancellationToken);
            if (product is null)
            {
                throw new EntityNotFoundException(nameof(Product), productId);
            }

            var productPrices = await _productPriceRepository.GetAllByProductIdAsync(productId, cancellationToken);

            return _mapper.Map<List<ProductPriceDto>>(productPrices);
        }

        public async Task<ProductPriceDto> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var productPrice = await _productPriceRepository.GetByIdAsync(id, cancellationToken);

            if (productPrice is null)
            {
                throw new EntityNotFoundException(nameof(ProductPrice), id);
            }

            return _mapper.Map<ProductPriceDto>(productPrice);
        }

        public async Task<ProductPriceDto> AddProductPriceForSellerAsync(AddProductPriceRequestDto request,
            CancellationToken cancellationToken)
        {
            var seller = await _sellerRepository.GetByIdAsync(request.SellerId, cancellationToken);
            if (seller is null)
            {
                throw new EntityNotFoundException(nameof(Seller), request.SellerId);
            }

            var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
            if (product is null)
            {
                throw new EntityNotFoundException(nameof(Product), request.ProductId);
            }

            var isPriceDefinedForThisDateBefore =await _productPriceRepository.IsPriceDefinedForDateAsync(request.ProductId,
                request.SellerId, request.PriceForDate, cancellationToken);
            if (isPriceDefinedForThisDateBefore)
            {
                throw new DuplicateProductPriceException();
            }

            var productPrice =
                await _productPriceRepository.AddAsync(_mapper.Map<ProductPrice>(request), cancellationToken);

            return _mapper.Map<ProductPriceDto>(productPrice);
        }

        public async Task<ProductPriceDto> UpdateAsync(UpdateProductPriceRequestDto request,
            CancellationToken cancellationToken)
        {
            var productPrice = await _productPriceRepository.GetByIdAsync(request.Id, cancellationToken);
            if (productPrice is null)
            {
                throw new EntityNotFoundException(nameof(ProductPrice), request.Id);
            }

            productPrice.UpdatePrice(request.NewPrice);
            var updatedProductPrice = await _productPriceRepository.UpdateAsync(productPrice, cancellationToken);

            return _mapper.Map<ProductPriceDto>(updatedProductPrice);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var productPrice = await _productPriceRepository.GetByIdAsync(id, cancellationToken);
            if (productPrice is null)
            {
                throw new EntityNotFoundException(nameof(ProductPrice), id);
            }

            await _productPriceRepository.DeleteAsync(productPrice, cancellationToken);
        }
    }
}