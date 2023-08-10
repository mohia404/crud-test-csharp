namespace Phonebook.Domain.Common.Errors;

public static partial class Errors
{
    public static class Product
    {
        public static Error NotFound => Error.NotFound(
            code: "Product.NotFound",
            description: "کالا یافت نشد.");
    }
}