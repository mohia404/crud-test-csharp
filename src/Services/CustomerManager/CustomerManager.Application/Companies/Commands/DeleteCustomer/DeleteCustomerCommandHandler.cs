using CustomerManager.Application.Common.Interfaces.Persistence;
using CustomerManager.Domain.Common.Errors;
using CustomerManager.Domain.Companies;
using CustomerManager.Domain.Companies.Entities;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CustomerManager.Application.Companies.Commands.DeleteCustomer;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, ErrorOr<Deleted>>
{
    private readonly ICompanyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteCustomerCommandHandler> _logger;

    public DeleteCustomerCommandHandler(ICompanyRepository repository, IUnitOfWork unitOfWork, ILogger<DeleteCustomerCommandHandler> logger)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<ErrorOr<Deleted>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        Company? company = await _repository.GetCompanyByIdAsync(request.CompanyId, cancellationToken);

        if (company is null)
            return Errors.Company.NotFound;

        Customer? customer = company.Customers.FirstOrDefault(x => x.Email == request.GivenEmail);

        if (customer is null)
            return Errors.Customer.NotFound;

        company.DeleteCustomer(customer.Id);

        _logger.LogInformation("Delete Customer in {companyId} with email: {customerEmail}",
            company.Id,
            request.GivenEmail);

        _repository.Update(company);

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return Result.Deleted;
    }
}