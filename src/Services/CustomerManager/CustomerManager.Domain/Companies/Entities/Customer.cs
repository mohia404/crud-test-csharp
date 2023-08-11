using CustomerManager.Domain.Common.Models;
using CustomerManager.Domain.Companies.ValueObjects;

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
        return new Customer(CustomerId.Create(Guid.NewGuid()), firstname, lastname, dateOfBirth, phoneNumber, email, bankAccountNumber);
    }

#pragma warning disable CS8618
    private Customer() { }
#pragma warning restore CS8618
}