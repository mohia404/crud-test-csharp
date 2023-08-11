using CustomerManager.Domain.Companies.ValueObjects;
using ErrorOr;
using MediatR;

namespace CustomerManager.Application.Companies.Commands.UpdateCustomer;

public record UpdateCustomerCommand(CompanyId CompanyId,
    string GivenEmail,
    string Firstname,
    string Lastname,
    DateTime DateOfBirth,
    string PhoneNumber,
    string Email,
    string BankAccountNumber) : IRequest<ErrorOr<Updated>>;