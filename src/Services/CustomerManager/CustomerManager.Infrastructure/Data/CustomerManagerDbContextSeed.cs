using CustomerManager.Domain.Companies;
using CustomerManager.Domain.Companies.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CustomerManager.Infrastructure.Data;

public class CustomerManagerDbContextSeed
{
    private readonly CustomerManagerDbContext _context;
    private readonly ILogger<CustomerManagerDbContextSeed> _logger;

    public CustomerManagerDbContextSeed(CustomerManagerDbContext context,
        ILogger<CustomerManagerDbContextSeed> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task SeedAsync(int retry = 0)
    {
        _logger.LogInformation("Seeding data.");
        _logger.LogInformation("DbContext Type: {providerName}", _context.Database.ProviderName);

        int retryForAvailability = retry;
        try
        {
            // apply migrations if connecting to a SQL database
            await _context.Database.MigrateAsync();

            if (!await _context.Companies.AnyAsync())
            {
                Company company = Company.Create(CompanyId.Create(new Guid("64f3573b-d584-4c36-9119-906e756c24ce")));

                // for test purpose
                company.AddNewCustomer("Mohammad", "Amini", DateTime.Now, 9158955560, "mohia1374@gmail.com", "123456789123456789123456");

                await _context.Companies.AddAsync(company);
            }
        }
        catch (Exception ex)
        {
            if (retryForAvailability >= 3) throw;
            retryForAvailability++;
            _logger.LogError(ex.Message);
            await SeedAsync(retryForAvailability);

            throw;
        }

        await _context.SaveChangesAsync();
    }
}