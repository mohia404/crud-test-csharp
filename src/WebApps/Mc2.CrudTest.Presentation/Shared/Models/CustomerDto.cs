namespace Mc2.CrudTest.Presentation.Shared.Models;

public class CustomerDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string BankAccountNumber { get; set; } = null!;
}