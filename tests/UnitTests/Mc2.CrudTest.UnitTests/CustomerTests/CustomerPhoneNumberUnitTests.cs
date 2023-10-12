using CustomerManager.Domain.Companies.Exceptions;
using CustomerManager.Domain.Companies.ValueObjects;
using FluentAssertions;

namespace Mc2.CrudTest.UnitTests.CustomerTests;

public class CustomerPhoneNumberUnitTests
{
    [Theory]
    [InlineData("+989121111111")]
    [InlineData("+1 414-719-8812")]
    public void CreateCustomerPhoneNumber_WhenPhoneIsCorrect_ShouldBeCreated(string phoneNumber)
    {
        CustomerPhoneNumber customerPhoneNumber = CustomerPhoneNumber.Create(phoneNumber);

        customerPhoneNumber.Value.Should().Be(phoneNumber);
    }

    [Theory]
    [InlineData("+982122511234")]
    public void CreatePhoneNumber_IfPhoneIsInvalid_ShouldThrowInvalidPhoneNumberException(string phoneNumber)
    {
        Assert.Throws<InvalidPhoneNumberException>(() => CustomerPhoneNumber.Create(phoneNumber));
    }
}