namespace CleanArchitecture.Domain.Exceptions;

public class PriceShouldBeGreaterThanZeroException : AppArgumentException
{
    public PriceShouldBeGreaterThanZeroException() : base("Price should be greater than zero","Price")
    {
    }
}