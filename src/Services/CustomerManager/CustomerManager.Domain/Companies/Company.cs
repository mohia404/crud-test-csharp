using CustomerManager.Domain.Common.Models;
using CustomerManager.Domain.Companies.Entities;
using CustomerManager.Domain.Companies.Exceptions;
using CustomerManager.Domain.Companies.ValueObjects;

namespace CustomerManager.Domain.Companies;

public class Company : AggregateRoot<CompanyId, Guid>
{
    private readonly List<Customer> _customers;
    public IReadOnlyList<Customer> Customers => _customers.AsReadOnly();

    private Company(CompanyId id, List<Customer>? customers = null) : base(id)
    {
        _customers = customers ?? new List<Customer>();
    }

    public static Company Create(CompanyId id)
    {
        return new Company(id);
    }

    public void AddNewCustomer(string firstname, string lastname, DateTime dateOfBirth, string phoneNumber, string email, string bankAccountNumber)
    {
        if (_customers.Any(x => x.Firstname == firstname && x.Lastname == lastname && x.DateOfBirth == dateOfBirth))
            throw new NameAndBirthAlreadyExistedException();

        if (_customers.Any(x => x.Email == email))
            throw new EmailAlreadyExistedException();

        Customer customer = Customer.Create(firstname, lastname, dateOfBirth, phoneNumber, email, bankAccountNumber);
        _customers.Add(customer);
    }

#pragma warning disable CS8618
    private Company() { }
#pragma warning restore CS8618
}