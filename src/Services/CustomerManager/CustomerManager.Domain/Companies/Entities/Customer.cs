﻿using CustomerManager.Domain.Common.Models;
using CustomerManager.Domain.Companies.Exceptions;
using CustomerManager.Domain.Companies.ValueObjects;
using System.Net.Mail;

namespace CustomerManager.Domain.Companies.Entities;

public class Customer : Entity<CustomerId>
{
    public string Firstname { get; private set; }
    public string Lastname { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public CustomerPhoneNumber PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string BankAccountNumber { get; private set; }

    private Customer(CustomerId id, string firstname, string lastname, DateTime dateOfBirth, CustomerPhoneNumber phoneNumber, string email, string bankAccountNumber) : base(id)
    {
        Firstname = firstname;
        Lastname = lastname;
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber;
        Email = email;
        BankAccountNumber = bankAccountNumber;
    }

    public static Customer Create(string firstname, string lastname, DateTime dateOfBirth, CustomerPhoneNumber phoneNumber, string email, string bankAccountNumber)
    {
        CheckEmail(email);
        CheckBankAccountNumber(bankAccountNumber);

        return new Customer(CustomerId.Create(Guid.NewGuid()), firstname, lastname, dateOfBirth, phoneNumber, email, bankAccountNumber);
    }

    public void Update(string firstname, string lastname, DateTime dateOfBirth, CustomerPhoneNumber phoneNumber, string email, string bankAccountNumber)
    {
        CheckEmail(email);
        CheckBankAccountNumber(bankAccountNumber);

        Firstname = firstname;
        Lastname = lastname;
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber;
        Email = email;
        BankAccountNumber = bankAccountNumber;
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