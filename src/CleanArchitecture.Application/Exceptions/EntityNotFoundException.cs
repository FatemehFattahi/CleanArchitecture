using CleanArchitecture.Domain.Exceptions;

namespace CleanArchitecture.Application.Exceptions;

public class EntityNotFoundException : AppException
{
    public EntityNotFoundException(string entityType, int id) : base(
        $"Entity Type '{entityType}' with Id '{id}' not Found!")
    {
    }
}