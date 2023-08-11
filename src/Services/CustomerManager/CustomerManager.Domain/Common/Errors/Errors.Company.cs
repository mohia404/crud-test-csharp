using ErrorOr;

namespace CustomerManager.Domain.Common.Errors;

public static partial class Errors
{
    public static class Company
    {
        public static Error NotFound => Error.NotFound(
            code: "Company.NotFound",
            description: "Company not found.");
    }
}