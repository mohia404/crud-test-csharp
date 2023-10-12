using CustomerManager.Domain.Companies;
using CustomerManager.Domain.Companies.ValueObjects;

namespace CustomerManager.Application.Common.Interfaces.Persistence;

public interface ICompanyRepository
{
    public Task<Company?> GetCompanyByIdAsync(CompanyId companyId, CancellationToken cancellationToken = default);
    public void Update(Company company);
}