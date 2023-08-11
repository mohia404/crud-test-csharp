using CustomerManager.Domain.Common.Errors;
using CustomerManager.Domain.Common.Exceptions;

namespace CustomerManager.Domain.Companies.Exceptions;

public class NameAndBirthAlreadyExistedException : ErrorException
{
    public NameAndBirthAlreadyExistedException() : base(Errors.Customer.NameAndBirthAlreadyExisted)
    {
    }
}