namespace CleanArchitecture.Domain.Models;

public class BaseEntity
{
    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }
}