namespace CleanArchitecture.Domain.Exceptions;

public class AppException : Exception, IAppException
{
    public AppException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }
}