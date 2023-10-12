using CustomerManager.Domain.Common.Models;

namespace CustomerManager.Domain.Companies.ValueObjects;

public sealed class CompanyId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private CompanyId(Guid value)
    {
        Value = value;
    }

    public static CompanyId Create(Guid value)
    {
        return new CompanyId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private CompanyId() { }
}