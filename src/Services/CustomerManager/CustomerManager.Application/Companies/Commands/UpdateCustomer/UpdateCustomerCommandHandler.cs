using CustomerManager.Application.Common.Interfaces.Persistence;
using CustomerManager.Domain.Common.Errors;
using CustomerManager.Domain.Companies;
using CustomerManager.Domain.Companies.Entities;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CustomerManager.Application.Companies.Commands.UpdateCustomer;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ErrorOr<Updated>>
{
    private readonly ICompanyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateCustomerCommandHandler> _logger;

    public UpdateCustomerCommandHandler(ICompanyRepository repository, IUnitOfWork unitOfWork, ILogger<UpdateCustomerCommandHandler> logger)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<ErrorOr<Updated>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        Company? company = await _repository.GetCompanyByIdAsync(request.CompanyId, cancellationToken);

        if (company is null)
            return Errors.Company.NotFound;

        Customer? customer = company.Customers.FirstOrDefault(x => x.Email == request.GivenEmail);

        if (customer is null)
            return Errors.Customer.NotFound;

        bool isPhoneNumberValid = ulong.TryParse(request.PhoneNumber, out ulong ulongPhoneNumber);

        if (!isPhoneNumberValid)
            return Errors.Customer.InvalidPhoneNumber;

        company.UpdateCustomer(customer.Id, request.Firstname, request.Lastname, request.DateOfBirth, ulongPhoneNumber, request.Email, request.BankAccountNumber);

        _logger.LogInformation("Update Customer in {companyId} with email: {customerEmail} to: {firstName} {lastName}, {email}",
            company.Id,
            request.GivenEmail,
            request.Firstname,
            request.Lastname,
            request.Email);

        _repository.Update(company);

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return Result.Updated;
    }
}