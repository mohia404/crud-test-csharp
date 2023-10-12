using CustomerManager.Application.Common.Interfaces.Persistence;
using CustomerManager.Domain.Companies;
using CustomerManager.Domain.Companies.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CustomerManager.Infrastructure.Data.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly CustomerManagerDbContext _dbContext;

    public CompanyRepository(CustomerManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Company?> GetCompanyByIdAsync(CompanyId companyId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Companies.FirstOrDefaultAsync(x => x.Id == companyId, cancellationToken);
    }

    public void Update(Company company)
    {
        _dbContext.Update(company);
    }
}