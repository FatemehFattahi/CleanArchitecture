using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Services;

namespace CleanArchitecture.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SellerController : ControllerBase
{
    private readonly ISellerService _sellerService;

    public SellerController(ISellerService sellerService)
    {
        _sellerService = sellerService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SellerDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var sellers = await _sellerService.GetAllAsync(cancellationToken);
        return Ok(sellers);
    }
}