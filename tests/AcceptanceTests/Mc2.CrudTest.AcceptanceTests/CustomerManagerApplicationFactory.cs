using CustomerManager.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.AcceptanceTests;

public class CustomerManagerApplicationFactory : WebApplicationFactory<CustomerManager.API.Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        string hostname = $"http://localhost:{GeneratePort()}";

        builder.UseUrls(hostname);

        builder.ConfigureServices(services =>
        {
            ServiceDescriptor? dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<CustomerManagerDbContext>));

            services.Remove(dbContextDescriptor);

            services.AddDbContext<CustomerManagerDbContext>(options =>
            {
                options.UseSqlServer("Data Source=localhost,1433;User ID=sa;Password=yourStrong(!)Password;Initial Catalog=CustomerManagerDbTest;TrustServerCertificate=True");
            });
        });
    }

    private static int GeneratePort()
    {
        return new Random().Next(5000, 32000);
    }
}