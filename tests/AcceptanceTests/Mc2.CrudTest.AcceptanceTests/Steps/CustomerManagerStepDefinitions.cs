using System.Diagnostics.CodeAnalysis;
using BoDi;
using CustomerManager.Contracts.Customers;
using CustomerManager.Domain.Companies;
using CustomerManager.Domain.Companies.ValueObjects;
using CustomerManager.Infrastructure.Data;
using FluentAssertions;
using Mc2.CrudTest.AcceptanceTests.Drivers.RowObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text;
using System.Text.Json;
using TechTalk.SpecFlow.Assist;
using System.Linq;
using Mc2.CrudTest.AcceptanceTests.Drivers.Http;

namespace Mc2.CrudTest.AcceptanceTests.Steps;

[Binding]
public class CustomerManagerStepDefinitions
{
    private readonly ScenarioContext _scenarioContext;
    private readonly CustomerManagerApplicationFactory _factory = new();
    private readonly HttpClient _client;
    private readonly CustomerManagerDbContext _databaseContext;

    private readonly CompanyId _companyId;

    public CustomerManagerStepDefinitions(ScenarioContext scenarioContext)
    {
        _companyId = CompanyId.Create(new Guid("64F3573B-D584-4C36-9119-906E756C24CE"));
        _scenarioContext = scenarioContext;
        _client = _factory.CreateClient();
        IServiceScope scope = _factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        _databaseContext = scope.ServiceProvider.GetRequiredService<CustomerManagerDbContext>();
    }

    [BeforeScenario]
    public async Task RegisterDependencies(IObjectContainer objectContainer)
    {
        await _databaseContext.Database.EnsureCreatedAsync();

        _databaseContext.Companies.RemoveRange(_databaseContext.Companies);
        await _databaseContext.Companies.AddAsync(
            Company.Create(_companyId));
        await _databaseContext.SaveChangesAsync();
    }

    [AfterScenario]
    public async Task Dispose(IObjectContainer objectContainer)
    {
        await _factory.DisposeAsync();
    }

    [Given(@"the following customers")]
    public async Task GivenTheFollowingCustomers(Table table)
    {
        Company company = await _databaseContext.Companies.FirstAsync(x => x.Id == _companyId);

        List<CustomerRow> givenCustomers = table.CreateSet<CustomerRow>().ToList();

        foreach (CustomerRow givenCustomer in givenCustomers)
        {
            company.AddNewCustomer(givenCustomer.Firstname,
                givenCustomer.Lastname,
                givenCustomer.DateOfBirth,
                ulong.Parse(givenCustomer.PhoneNumber),
                givenCustomer.Email,
                givenCustomer.BankAccountNumber);
        }

        await _databaseContext.SaveChangesAsync();
    }

    [When(@"i try to create customer")]
    public async Task WhenITryToCreateCustomer(Table table)
    {
        CustomerRow givenCustomer = table.CreateInstance<CustomerRow>();
        _scenarioContext["response"] = await _client.PostAsync($"/api/companies/{_companyId.Value}/customers", ToJson(givenCustomer));
    }

    [Then(@"the customers should be")]
    public async Task ThenTheCustomersShouldBe(Table table)
    {
        HttpResponseMessage? response = _scenarioContext["response"] as HttpResponseMessage;
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(HttpStatusCode.NoContent);

        Company company = await _databaseContext.Companies.FirstAsync(x => x.Id == _companyId);

        List<CustomerRow> expectedCustomers = table.CreateSet<CustomerRow>().ToList();
        List<CustomerResult> customers = company.Customers.Select(givenCustomer => new CustomerResult(givenCustomer.Firstname,
            givenCustomer.Lastname,
            givenCustomer.DateOfBirth,
            givenCustomer.PhoneNumber.ToString(),
            givenCustomer.Email,
            givenCustomer.BankAccountNumber)).ToList();

        customers.Should().BeEquivalentTo(expectedCustomers);
    }

    [Then(@"i should get invalid phone number error")]
    public async Task ThenIShouldGetInvalidPhoneNumberError()
    {
        HttpResponseMessage? response = _scenarioContext["response"] as HttpResponseMessage;

        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await CheckValidationErrorAsync(response, "Customer.InvalidPhoneNumber");
    }

    [Then(@"i should get customer exists error")]
    public async Task ThenIShouldGetCustomerExistsError()
    {
        HttpResponseMessage? response = _scenarioContext["response"] as HttpResponseMessage;

        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(HttpStatusCode.Conflict);

        await CheckErrorAsync(response, "Customer.NameAndBirthConflict");
    }

    [Then(@"i should get email exists error")]
    public async Task ThenIShouldGetEmailExistsError()
    {
        HttpResponseMessage? response = _scenarioContext["response"] as HttpResponseMessage;

        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(HttpStatusCode.Conflict);

        await CheckErrorAsync(response, "Customer.EmailConflict");
    }

    [When(@"i try to delete customer with email '([^']*)'")]
    public void WhenITryToDeleteCustomerWithEmail(string email)
    {
    }

    [Then(@"i should get customer do not exist error")]
    public void ThenIShouldGetCustomerDoNotExistError()
    {
    }

    [When(@"i try to get customers")]
    public async Task WhenITryToGetCustomers()
    {
        _scenarioContext["response"] = await _client.GetAsync($"/api/companies/{_companyId.Value}/customers");
    }

    [Then(@"i should get following customers")]
    public async Task ThenIShouldGetFollowingCustomers(Table table)
    {
        HttpResponseMessage? response = _scenarioContext["response"] as HttpResponseMessage;
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(HttpStatusCode.OK);

        List<CustomerResult>? responseCustomers = await FromHttpResponseMessageAsync<List<CustomerResult>>(response);
        responseCustomers.Should().NotBeNull();

        List<CustomerRow> expectedCustomers = table.CreateSet<CustomerRow>().ToList();

        responseCustomers!.Should().BeEquivalentTo(expectedCustomers);
    }

    [When(@"i try to get customer with email '([^']*)'")]
    public void WhenITryToGetCustomerWithEmail(string email)
    {
    }

    [Then(@"i should get following customer")]
    public void ThenIShouldGetFollowingCustomer(Table table)
    {
    }

    [When(@"i try to update following customer")]
    public void WhenITryToUpdateFollowingCustomer(Table table)
    {
    }

    private static async Task<T?> FromHttpResponseMessageAsync<T>(HttpResponseMessage result)
    {
        return JsonSerializer.Deserialize<T>(await result.Content.ReadAsStringAsync(), new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }

    private static StringContent ToJson(object obj)
    {
        return new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");
    }

    private async Task CheckResponseErrorAsync(HttpResponseMessage result, ProblemDetailsWithErrors problemDetailsWithErrors)
    {
        JsonSerializerOptions options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        string output = await result.Content.ReadAsStringAsync();

        ProblemDetailsWithErrors? error = JsonSerializer.Deserialize<ProblemDetailsWithErrors>(output, options);

        error.Should().NotBeNull();
        error!.Should().BeEquivalentTo(problemDetailsWithErrors);
    }

    private static async Task CheckValidationErrorAsync(HttpResponseMessage response, string error)
    {
        JsonSerializerOptions options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        string output = await response.Content.ReadAsStringAsync();

        ValidationProblemDetailsWithErrors? validationError = JsonSerializer.Deserialize<ValidationProblemDetailsWithErrors>(output, options);

        validationError.Should().NotBeNull();
        validationError!.Errors.Select(x => x.Key).Should().Contain(error);
    }

    private static async Task CheckErrorAsync(HttpResponseMessage response, string error)
    {
        JsonSerializerOptions options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        string output = await response.Content.ReadAsStringAsync();

        ProblemDetailsWithErrors? validationError = JsonSerializer.Deserialize<ProblemDetailsWithErrors>(output, options);

        validationError.Should().NotBeNull();
        validationError!.ErrorCodes.Should().Contain(error);
    }
}