using CleanArchitecture.Domain.Models;

namespace CleanArchitecture.Application.Dtos;

public record AddProductRequestDto(string Ean, string Sku, string Name, string Description);