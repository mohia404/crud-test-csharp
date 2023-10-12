using CustomerManager.Domain.Common.Errors;
using CustomerManager.Domain.Common.Exceptions;

namespace CustomerManager.Domain.Companies.Exceptions;

public class InvalidBankAccountNumberException : ErrorException
{
    public InvalidBankAccountNumberException() : base(Errors.Customer.InvalidBankAccountNumber)
    {
    }
}