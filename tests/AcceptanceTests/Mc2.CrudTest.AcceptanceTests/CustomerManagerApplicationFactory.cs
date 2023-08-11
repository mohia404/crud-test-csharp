using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Mc2.CrudTest.AcceptanceTests;

public class CustomerManagerApplicationFactory : WebApplicationFactory<CustomerManager.API.Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        string hostname = $"http://localhost:{GeneratePort()}";

        Dictionary<string, string> configurationValues = new()
        {
            { "DefaultConnection", "Data Source=localhost,1433;User ID=sa;Password=yourStrong(!)Password;Initial Catalog=CustomerDbTest;TrustServerCertificate=True" }
        };

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(configurationValues!)
            .Build();

        builder.UseUrls(hostname);

        builder
            .UseConfiguration(configuration);
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        // Add mock/test services to the builder here
        builder.ConfigureServices(services =>
        {

        });

        return base.CreateHost(builder);
    }

    private static int GeneratePort()
    {
        return new Random().Next(5000, 32000);
    }
}