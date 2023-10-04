using CustomerManager.Domain.Common.Models;
using CustomerManager.Domain.Companies.Exceptions;
using PhoneNumbers;

namespace CustomerManager.Domain.Companies.ValueObjects;

public sealed class CustomerPhoneNumber : ValueObject
{
    public string Value { get; }

    private CustomerPhoneNumber(string value)
    {
        Value = value;
    }

    private static void CheckPhoneNumber(string phoneNumber)
    {
        try
        {
            PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
            PhoneNumber phoneNumberLib = phoneNumberUtil.Parse(phoneNumber, null);

            if (phoneNumberUtil.GetNumberType(phoneNumberLib) != PhoneNumberType.MOBILE &&
                phoneNumberUtil.GetNumberType(phoneNumberLib) != PhoneNumberType.FIXED_LINE_OR_MOBILE)
                throw new InvalidPhoneNumberException();

            if (!phoneNumberUtil.IsValidNumber(phoneNumberLib))
                throw new InvalidPhoneNumberException();
        }
        catch (NumberParseException)
        {
            throw new InvalidPhoneNumberException();
        }
    }

    public static CustomerPhoneNumber Create(string value)
    {
        CheckPhoneNumber(value);
        return new CustomerPhoneNumber(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value;
    }

    public static implicit operator string(CustomerPhoneNumber phoneNumber)
    {
        return phoneNumber.ToString();
    }

    public static explicit operator CustomerPhoneNumber(string phoneNumber)
    {
        return Create(phoneNumber);
    }
}