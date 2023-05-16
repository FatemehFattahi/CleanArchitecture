namespace CleanArchitecture.Application.Dtos;

public record AddProductPriceRequestDto(int ProductId,
    int SellerId,
    decimal Price,
    DateOnly PriceForDate);