using CustomerManager.Domain.Common.Errors;
using CustomerManager.Domain.Common.Exceptions;

namespace CustomerManager.Domain.Companies.Exceptions;

public class CustomerNotExistException : ErrorException
{
    public CustomerNotExistException() : base(Errors.Customer.NotFound)
    {
    }
}