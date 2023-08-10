using Phonebook.Domain.Common.Models;

namespace Phonebook.Domain.Phonebook.ValueObjects;

public sealed class CustomerId : ValueObject
{
    public Guid Value { get; }

    private CustomerId(Guid value)
    {
        Value = value;
    }

    public static CustomerId Create(Guid value)
    {
        return new CustomerId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}