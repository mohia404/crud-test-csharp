namespace CustomerManager.Contracts.Customers;

public record CustomerResult(string Firstname,
    string Lastname,
    DateTime DateOfBirth,
    string PhoneNumber,
    string Email,
    string BankAccountNumber);