using CustomerManager.Domain.Companies.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManager.API.Controllers;

[Route("api/companies/{companyId:string}/customers")]
public class CustomerController : ApiController
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCustomers(string companyId, CancellationToken cancellationToken)
    {
        var command = new GetAllCustomersQuery(CompanyId: CompanyId.Create(companyId));

        var result = await _mediator.Send(command, cancellationToken);

        return result.Match(
            Microsoft.AspNetCore.Http.HttpResults.Ok,
            Problem);
    }
}