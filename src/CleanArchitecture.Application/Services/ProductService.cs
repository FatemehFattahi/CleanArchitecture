using AutoMapper;
using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Repositories;
using CleanArchitecture.Domain.Models;

namespace CleanArchitecture.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<ProductDto> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(id, cancellationToken);
            
            if (product is null)
            {
                throw new EntityNotFoundException(nameof(Product), id);
            }

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> AddAsync(AddProductRequestDto request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.AddAsync(_mapper.Map<Product>(request), cancellationToken);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> UpdateAsync(UpdateProductRequestDto request, int productId, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(productId, cancellationToken);
            if (product is null)
            {
                throw new EntityNotFoundException(nameof(Product), productId);
            }

            var productToUpdate = new Product(request.Ean, request.Sku, request.Name, request.Description)
            {
                Id = productId
            };

            var updatedProduct = await _productRepository.UpdateAsync(productToUpdate, cancellationToken);
            return _mapper.Map<ProductDto>(updatedProduct);
        }
    }
}