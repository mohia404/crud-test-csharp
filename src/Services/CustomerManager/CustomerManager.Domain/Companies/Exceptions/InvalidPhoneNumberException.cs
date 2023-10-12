using CustomerManager.Domain.Common.Errors;
using CustomerManager.Domain.Common.Exceptions;

namespace CustomerManager.Domain.Companies.Exceptions;

public class InvalidPhoneNumberException : ErrorException
{
    public InvalidPhoneNumberException() : base(Errors.Customer.InvalidPhoneNumber)
    {
    }
}