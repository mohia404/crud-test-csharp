using CustomerManager.Contracts.Customers;
using CustomerManager.Domain.Companies.ValueObjects;
using ErrorOr;
using MediatR;

namespace CustomerManager.Application.Companies.Queries.GetAllCustomers;

public record GetAllCustomersQuery(CompanyId CompanyId) : IRequest<ErrorOr<List<CustomerResult>>>;