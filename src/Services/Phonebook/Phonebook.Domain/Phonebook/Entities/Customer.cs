using Phonebook.Domain.Common.Models;
using Phonebook.Domain.Phonebook.ValueObjects;

namespace Phonebook.Domain.Phonebook.Entities;

public class Customer : Entity<CustomerId>
{
    public string Firstname { get; }
    public string Lastname { get; }
    public DateTime DateOfBirth { get; }
    public string PhoneNumber { get; }
    public string Email { get; }
    public string BankAccountNumber { get; }

    private Customer(CustomerId id, string firstname, string lastname, DateTime dateOfBirth, string phoneNumber, string email, string bankAccountNumber) : base(id)
    {
        Firstname = firstname;
        Lastname = lastname;
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber;
        Email = email;
        BankAccountNumber = bankAccountNumber;
    }

    public static Customer Create(CustomerId id, string firstname, string lastname, DateTime dateOfBirth, string phoneNumber, string email, string bankAccountNumber)
    {
        return new Customer(id, firstname, lastname, dateOfBirth, phoneNumber, email, bankAccountNumber);
    }

#pragma warning disable CS8618
    private Customer() { }
#pragma warning restore CS8618
}