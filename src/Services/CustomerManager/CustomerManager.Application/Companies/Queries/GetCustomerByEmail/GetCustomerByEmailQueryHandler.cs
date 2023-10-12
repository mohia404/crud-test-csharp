using CustomerManager.Application.Common.Interfaces.Persistence;
using CustomerManager.Contracts.Customers;
using CustomerManager.Domain.Common.Errors;
using CustomerManager.Domain.Companies;
using CustomerManager.Domain.Companies.Entities;
using ErrorOr;
using MapsterMapper;
using MediatR;

namespace CustomerManager.Application.Companies.Queries.GetCustomerByEmail;

public class GetCustomerByEmailQueryHandler : IRequestHandler<GetCustomerByEmailQuery, ErrorOr<CustomerResult>>
{
    private readonly ICompanyRepository _repository;
    private readonly IMapper _mapper;

    public GetCustomerByEmailQueryHandler(ICompanyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<CustomerResult>> Handle(GetCustomerByEmailQuery request, CancellationToken cancellationToken)
    {
        Company? company = await _repository.GetCompanyByIdAsync(request.CompanyId, cancellationToken);

        if (company is null)
            return Errors.Company.NotFound;

        Customer? customer = company.Customers.FirstOrDefault(x => x.Email == request.Email);

        if (customer is null)
            return Errors.Customer.NotFound;

        return _mapper.Map<CustomerResult>(customer);
    }
}