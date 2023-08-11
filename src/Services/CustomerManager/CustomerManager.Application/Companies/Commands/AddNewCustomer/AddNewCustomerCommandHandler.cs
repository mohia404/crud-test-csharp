using CustomerManager.Application.Common.Interfaces.Persistence;
using CustomerManager.Domain.Common.Errors;
using CustomerManager.Domain.Companies;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CustomerManager.Application.Companies.Commands.AddNewCustomer;

public class AddNewCustomerCommandHandler : IRequestHandler<AddNewCustomerCommand, ErrorOr<Created>>
{
    private readonly ICompanyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddNewCustomerCommandHandler> _logger;

    public AddNewCustomerCommandHandler(ICompanyRepository repository, IUnitOfWork unitOfWork, ILogger<AddNewCustomerCommandHandler> logger)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<ErrorOr<Created>> Handle(AddNewCustomerCommand request, CancellationToken cancellationToken)
    {
        Company? company = await _repository.GetCompanyByIdAsync(request.CompanyId, cancellationToken);

        if (company is null)
            return Errors.Company.NotFound;

        bool isPhoneNumberValid = ulong.TryParse(request.PhoneNumber, out ulong ulongPhoneNumber);

        if (!isPhoneNumberValid)
            return Errors.Customer.InvalidPhoneNumber;

        company.AddNewCustomer(request.Firstname, request.Lastname, request.DateOfBirth, ulongPhoneNumber, request.Email, request.BankAccountNumber);

        _logger.LogInformation("Create New Customer for {companyId}: {firstName} {lastName}, {email}",
            company.Id,
            request.Firstname,
            request.Lastname,
            request.Email);

        _repository.Update(company);

        await _unitOfWork.SaveChangeAsync(cancellationToken);

        return Result.Created;
    }
}