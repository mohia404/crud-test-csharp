using ErrorOr;

namespace Phonebook.Domain.Common.Errors;

public static partial class Errors
{
    public static class Customer
    {
        public static Error NotFound => Error.NotFound(
            code: "Customer.NotFound",
            description: "Customer not found.");
    }
}