using CustomerManager.Contracts.Customers;
using CustomerManager.Domain.Companies.ValueObjects;
using ErrorOr;
using MediatR;

namespace CustomerManager.Application.Companies.Queries.GetCustomerByEmail;

public record GetCustomerByEmailQuery(CompanyId CompanyId, string Email) : IRequest<ErrorOr<CustomerResult>>;