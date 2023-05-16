namespace CleanArchitecture.Application.Dtos;

public record ProductPriceDto(int Id, int ProductId, int SellerId, decimal Price, DateOnly PriceForDate);