using CustomerManager.Contracts.Customers;
using CustomerManager.Domain.Companies.ValueObjects;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using CustomerManager.Application.Companies.Queries.GetAllCustomers;
using CustomerManager.Application.Companies.Commands.AddNewCustomer;

namespace CustomerManager.API.Controllers;

[Route("api/companies/{companyId:guid}/customers")]
public class CustomerController : ApiController
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<CustomerResult>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAllCustomers(Guid companyId, CancellationToken cancellationToken)
    {
        GetAllCustomersQuery command = new(CompanyId: CompanyId.Create(companyId));

        ErrorOr<List<CustomerResult>> result = await _mediator.Send(command, cancellationToken);

        return result.Match(
            Ok,
            Problem);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewCustomer(Guid companyId, CustomerRequest request, CancellationToken cancellationToken)
    {
        AddNewCustomerCommand command = new(CompanyId: CompanyId.Create(companyId),
            request.Firstname,
            request.Lastname,
            request.DateOfBirth,
            request.PhoneNumber,
            request.Email,
            request.BankAccountNumber);

        ErrorOr<Created> result = await _mediator.Send(command, cancellationToken);

        return result.Match(
            _ => NoContent(),
            Problem);
    }
}