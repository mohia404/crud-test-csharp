using CustomerManager.Application.Common.Interfaces.Persistence;
using CustomerManager.Contracts.Customers;
using CustomerManager.Domain.Common.Errors;
using CustomerManager.Domain.Companies;
using ErrorOr;
using MapsterMapper;
using MediatR;

namespace CustomerManager.Application.Companies.Queries.GetAllCustomers;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, ErrorOr<List<CustomerResult>>>
{
    private readonly ICompanyRepository _repository;
    private readonly IMapper _mapper;

    public GetAllCustomersQueryHandler(ICompanyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<List<CustomerResult>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        Company? company = await _repository.GetCompanyByIdAsync(request.CompanyId, cancellationToken);

        if (company is null)
            return Errors.Company.NotFound;
        
        return _mapper.Map<List<CustomerResult>>(company.Customers);
    }
}