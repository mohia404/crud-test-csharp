using CustomerManager.Domain.Companies.ValueObjects;
using ErrorOr;
using MediatR;

namespace CustomerManager.Application.Companies.Commands.DeleteCustomer;

public record DeleteCustomerCommand(CompanyId CompanyId,
    string GivenEmail) : IRequest<ErrorOr<Deleted>>;