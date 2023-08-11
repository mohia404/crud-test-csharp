namespace CustomerManager.Application.Common.Interfaces.Persistence;

public interface IUnitOfWork
{
    public Task SaveChangeAsync(CancellationToken cancellationToken = default);
}