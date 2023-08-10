using Phonebook.Domain.Common.Models;
using Phonebook.Domain.Phonebook.Entities;
using Phonebook.Domain.Phonebook.ValueObjects;

namespace Phonebook.Domain.Phonebook;

public class Phonebook : AggregateRoot<PhonebookId, Guid>
{
    private readonly List<Customer> _customers;
    public IReadOnlyList<Customer> Customers => _customers.AsReadOnly();

    private Phonebook(PhonebookId id, List<Customer>? customers = null) : base(id)
    {
        _customers = customers ?? new List<Customer>();
    }

    public static Phonebook Create(PhonebookId id)
    {
        return new Phonebook(id);
    }

#pragma warning disable CS8618
    private Phonebook() { }
#pragma warning restore CS8618
}