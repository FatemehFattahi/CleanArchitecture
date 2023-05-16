namespace CleanArchitecture.Domain.Exceptions;

public class ArgumentNullOrEmptyException : AppArgumentException
{
    public ArgumentNullOrEmptyException(string argumentName) : base($"{argumentName} should not be null or empty!",
        argumentName)
    {
    }
}