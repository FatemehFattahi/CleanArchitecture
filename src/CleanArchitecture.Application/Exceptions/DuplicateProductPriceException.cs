using CleanArchitecture.Domain.Exceptions;

namespace CleanArchitecture.Application.Exceptions
{
    internal class DuplicateProductPriceException : AppArgumentException
    {
        public DuplicateProductPriceException() : base(
            "You defined a price for this product and seller for this date before!")
        {
        }
    }
}