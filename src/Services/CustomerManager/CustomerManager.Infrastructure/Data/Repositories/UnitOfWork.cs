using CustomerManager.Application.Common.Interfaces.Persistence;

namespace CustomerManager.Infrastructure.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly CustomerManagerDbContext _dbContext;

    public UnitOfWork(CustomerManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveChangeAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}