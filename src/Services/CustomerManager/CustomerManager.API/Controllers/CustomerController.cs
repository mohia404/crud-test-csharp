using CustomerManager.Contracts.Customers;
using CustomerManager.Domain.Companies.ValueObjects;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using CustomerManager.Application.Companies.Queries.GetAllCustomers;

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
}