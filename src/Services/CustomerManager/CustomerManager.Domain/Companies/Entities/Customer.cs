using CustomerManager.Domain.Common.Models;
using CustomerManager.Domain.Companies.Exceptions;
using CustomerManager.Domain.Companies.ValueObjects;
using PhoneNumbers;
using System.Net.Mail;

namespace CustomerManager.Domain.Companies.Entities;

public class Customer : Entity<CustomerId>
{
    public string Firstname { get; }
    public string Lastname { get; }
    public DateTime DateOfBirth { get; }
    public ulong PhoneNumber { get; }
    public string Email { get; }
    public string BankAccountNumber { get; }

    private Customer(CustomerId id, string firstname, string lastname, DateTime dateOfBirth, ulong phoneNumber, string email, string bankAccountNumber) : base(id)
    {
        Firstname = firstname;
        Lastname = lastname;
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber;
        Email = email;
        BankAccountNumber = bankAccountNumber;
    }

    public static Customer Create(string firstname, string lastname, DateTime dateOfBirth, ulong phoneNumber, string email, string bankAccountNumber)
    {
        CheckPhoneNumber(phoneNumber.ToString());
        CheckEmail(email);
        CheckBankAccountNumber(bankAccountNumber);

        return new Customer(CustomerId.Create(Guid.NewGuid()), firstname, lastname, dateOfBirth, phoneNumber, email, bankAccountNumber);
    }

    private static void CheckPhoneNumber(string phoneNumber)
    {
        try
        {
            PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
            PhoneNumber phoneNumberLib = phoneNumberUtil.Parse(phoneNumber, "IR");
            if (!phoneNumberUtil.IsValidNumber(phoneNumberLib))
                throw new InvalidPhoneNumberException();
        }
        catch (NumberParseException)
        {
            throw new InvalidPhoneNumberException();
        }
    }

    private static void CheckEmail(string email)
    {
        try
        {
            _ = new MailAddress(email);
        }
        catch
        {
            throw new InvalidEmailException();
        }
    }

    private static void CheckBankAccountNumber(string bankAccountNumber)
    {
        if (bankAccountNumber.Length != 24 || !bankAccountNumber.All(char.IsDigit))
            throw new InvalidBankAccountNumberException();
    }

#pragma warning disable CS8618
    private Customer() { }
#pragma warning restore CS8618
}