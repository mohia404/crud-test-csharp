using ErrorOr;

namespace CustomerManager.Domain.Common.Errors;

public static partial class Errors
{
    public static class Customer
    {
        public static Error NotFound => Error.NotFound(
            code: "Customer.NotFound",
            description: "Customer not found.");

        public static Error NameAndBirthAlreadyExisted => Error.Conflict(
            code: "Customer.NameAndBirthConflict",
            description: "Another customer with given first name, last name and date of birth already existed.");

        public static Error EmailAlreadyExisted => Error.Conflict(
            code: "Customer.EmailConflict",
            description: "Another customer with given email already existed.");

        public static Error InvalidPhoneNumber => Error.Validation(
            code: "Customer.InvalidPhoneNumber",
            description: "Customer phone number is not valid.");
    }
}