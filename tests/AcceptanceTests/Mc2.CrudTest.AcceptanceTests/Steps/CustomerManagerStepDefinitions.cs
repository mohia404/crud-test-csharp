using BoDi;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using CustomerManager.Infrastructure.Data;

namespace Mc2.CrudTest.AcceptanceTests.Steps;

[Binding]
public class CustomerManagerStepDefinitions
{
    private readonly ScenarioContext _scenarioContext;
    public CustomerManagerApplicationFactory _factory = new();
    public HttpClient _client { get; set; } = null!;
    private readonly CustomerManagerDbContext _databaseContext;

    public CustomerManagerStepDefinitions(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        var scope = _factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        _databaseContext = scope.ServiceProvider.GetRequiredService<CustomerManagerDbContext>();
    }

    [BeforeScenario]
    public void RegisterDependencies(IObjectContainer objectContainer)
    {
        _databaseContext.Database.EnsureCreated();

        _databaseContext.Phonebooks.RemoveRange(_databaseContext.Phonebooks);
        _databaseContext.SaveChanges();

        _client = _factory.CreateClient();
    }

    [AfterScenario]
    public void Dispose(IObjectContainer objectContainer)
    {
        _factory.Dispose();
    }

    [Given(@"the following customers")]
    public void GivenTheFollowingCustomers(Table table)
    {
    }

    [When(@"i try to create customer")]
    public void WhenITryToCreateCustomer(Table table)
    {

    }

    [Then(@"the customers should be")]
    public async Task ThenTheCustomersShouldBe(Table table)
    {
        HttpResponseMessage response = await _client.GetAsync("/swagger/index.html");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        //var expected = table.CreateSet<Product>();
        //var actual = await _response.Content.ReadFromJsonAsync<List<Product>>();

        //actual.Should().BeEquivalentTo(expected);
    }

    [Then(@"i should get invalid phone number error")]
    public void ThenIShouldGetInvalidPhoneNumberError()
    {
    }

    [Then(@"i should get customer exists error")]
    public void ThenIShouldGetCustomerExistsError()
    {
    }

    [Then(@"i should get email exists error")]
    public void ThenIShouldGetEmailExistsError()
    {
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
    public void WhenITryToGetCustomers()
    {
    }

    [Then(@"i should get following customers")]
    public void ThenIShouldGetFollowingCustomers(Table table)
    {
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
}