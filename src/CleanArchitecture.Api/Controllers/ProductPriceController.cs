using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Services;

namespace CleanArchitecture.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductPriceController : ControllerBase
{
    private readonly IProductPriceService _productPriceService;

    public ProductPriceController(IProductPriceService productPriceService)
    {
        _productPriceService = productPriceService;
    }

    [HttpGet("Product/{productId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductPriceDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllByProductId(int productId, CancellationToken cancellationToken)
    {
        var productPrices = await _productPriceService.GetAllPricesForProductAsync(productId, cancellationToken);
        return Ok(productPrices);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductPriceDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        var productPrice = await _productPriceService.GetByIdAsync(id, cancellationToken);

        return Ok(productPrice);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProductPriceDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(AddProductPriceRequestDto request, CancellationToken cancellationToken)
    {
        var productPrice = await _productPriceService.AddProductPriceForSellerAsync(request, cancellationToken);

        return CreatedAtAction("Get", "ProductPrice", new { productPrice.Id }, productPrice);
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductPriceDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProductPrice(UpdateProductPriceRequestDto request, CancellationToken cancellationToken)
    {
        var productPrice = await _productPriceService.UpdateAsync(request, cancellationToken);

        return Ok(productPrice);
    }
}