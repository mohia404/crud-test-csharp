using Phonebook.Domain.Common.Models;

namespace Phonebook.Domain.Phonebooks.ValueObjects;

public sealed class PhonebookId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private PhonebookId(Guid value)
    {
        Value = value;
    }

    public static PhonebookId Create(Guid value)
    {
        return new PhonebookId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private PhonebookId() { }
}