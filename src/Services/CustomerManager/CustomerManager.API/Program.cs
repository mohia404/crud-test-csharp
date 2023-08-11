using CustomerManager.API;
using CustomerManager.Application;
using CustomerManager.Infrastructure;
using CustomerManager.Infrastructure.Data;

const string corsPolicy = "CorsPolicy";

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: corsPolicy, policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowedToAllowWildcardSubdomains();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

IWebHostEnvironment? hostEnvironment = app.Services.GetService<IWebHostEnvironment>();
ILoggerFactory loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
ILogger logger = loggerFactory.CreateLogger(nameof(app));
logger.LogInformation("Starting in environment {hostEnvironment}", hostEnvironment?.EnvironmentName);
try
{
    using IServiceScope scope = app.Services.CreateScope();
    IServiceProvider services = scope.ServiceProvider;
    CustomerManagerDbContextSeed seedService = services.GetRequiredService<CustomerManagerDbContextSeed>();
    await seedService.SeedAsync();
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occurred seeding the DB.");
}

app.UseCors(corsPolicy);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/error");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

namespace CustomerManager.API
{
    public partial class Program{}
}