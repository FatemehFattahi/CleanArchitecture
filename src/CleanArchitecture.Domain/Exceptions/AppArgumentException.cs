namespace CleanArchitecture.Domain.Exceptions;

public class AppArgumentException : ArgumentException, IAppException
{
    public AppArgumentException(string message, string? parameterName = null, Exception? innerException = null)
        : base(message, parameterName, innerException)
    {
    }
}