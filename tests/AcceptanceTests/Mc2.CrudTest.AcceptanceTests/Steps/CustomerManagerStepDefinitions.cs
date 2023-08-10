namespace Mc2.CrudTest.AcceptanceTests.Steps;

[Binding]
public class CustomerManagerStepDefinitions
{
    [Given(@"the following customers")]
    public void GivenTheFollowingCustomers(Table table)
    {
        throw new PendingStepException();
    }

    [When(@"i try to create customer")]
    public void WhenITryToCreateCustomer(Table table)
    {
        throw new PendingStepException();
    }

    [Then(@"the customers should be")]
    public void ThenTheCustomersShouldBe(Table table)
    {
        throw new PendingStepException();
    }

    [Then(@"i should get invalid phone number error")]
    public void ThenIShouldGetInvalidPhoneNumberError()
    {
        throw new PendingStepException();
    }

    [Then(@"i should get customer exists error")]
    public void ThenIShouldGetCustomerExistsError()
    {
        throw new PendingStepException();
    }

    [Then(@"i should get email exists error")]
    public void ThenIShouldGetEmailExistsError()
    {
        throw new PendingStepException();
    }

    [When(@"i try to delete customer with email mohia(.*)@gmail\.com")]
    public void WhenITryToDeleteCustomerWithEmailMohiaGmail_Com(int p0)
    {
        throw new PendingStepException();
    }

    [When(@"i try to delete customer with id '([^']*)'")]
    public void WhenITryToDeleteCustomerWithId(string p0)
    {
        throw new PendingStepException();
    }

    [Then(@"i should get customer do not exist error")]
    public void ThenIShouldGetCustomerDoNotExistError()
    {
        throw new PendingStepException();
    }

    [When(@"i try to get customers")]
    public void WhenITryToGetCustomers()
    {
        throw new PendingStepException();
    }

    [Then(@"i should get following customers")]
    public void ThenIShouldGetFollowingCustomers(Table table)
    {
        throw new PendingStepException();
    }

    [When(@"i try to get customer with email mohia(.*)@gmail\.com")]
    public void WhenITryToGetCustomerWithEmailMohiaGmail_Com(int p0)
    {
        throw new PendingStepException();
    }

    [Then(@"i should get following customer")]
    public void ThenIShouldGetFollowingCustomer(Table table)
    {
        throw new PendingStepException();
    }

    [When(@"i try to update following customer")]
    public void WhenITryToUpdateFollowingCustomer(Table table)
    {
        throw new PendingStepException();
    }
}