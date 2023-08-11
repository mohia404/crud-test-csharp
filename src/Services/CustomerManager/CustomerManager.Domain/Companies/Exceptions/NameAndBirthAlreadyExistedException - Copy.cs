using CustomerManager.Domain.Common.Errors;
using CustomerManager.Domain.Common.Exceptions;

namespace CustomerManager.Domain.Companies.Exceptions;

public class EmailAlreadyExistedException : ErrorException
{
    public EmailAlreadyExistedException() : base(Errors.Customer.EmailAlreadyExisted)
    {
    }
}