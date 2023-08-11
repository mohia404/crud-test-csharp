using Blazored.Toast;
using Mc2.CrudTest.Presentation.Client.Common.Mapping;
using Mc2.CrudTest.Presentation.Client.Services;
using Mc2.CrudTest.Presentation.Shared;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;

namespace Mc2.CrudTest.Presentation.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NGaF5cXmVCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdgWXldcHRXQmBcUkJ0V0Q=");

            WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            CompanyConfiguration baseUrlConfig = new();
            builder.Configuration.Bind(CompanyConfiguration.ConfigName, baseUrlConfig);
            builder.Services.AddScoped(sp => baseUrlConfig);

            builder.Services.AddMappings();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddHttpClient("backend",
                client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            builder.Services.AddBlazoredToast();
            builder.Services.AddSyncfusionBlazor();

            builder.Services.AddScoped<HttpService>();
            builder.Services.AddScoped<CustomerService>();

            await builder.Build().RunAsync();
        }
    }
}