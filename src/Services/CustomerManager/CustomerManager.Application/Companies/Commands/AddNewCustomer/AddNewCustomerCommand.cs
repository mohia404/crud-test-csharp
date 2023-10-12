using CustomerManager.Domain.Companies.ValueObjects;
using ErrorOr;
using MediatR;

namespace CustomerManager.Application.Companies.Commands.AddNewCustomer;

public record AddNewCustomerCommand(CompanyId CompanyId,
    string Firstname,
    string Lastname,
    DateTime DateOfBirth,
    string PhoneNumber,
    string Email,
    string BankAccountNumber) : IRequest<ErrorOr<Created>>;