using CustomerManager.Domain.Common.Errors;
using CustomerManager.Domain.Common.Exceptions;

namespace CustomerManager.Domain.Companies.Exceptions;

public class InvalidEmailException : ErrorException
{
    public InvalidEmailException() : base(Errors.Customer.InvalidEmail)
    {
    }
}