namespace CustomerManager.Contracts.Customers;

public record CustomerRequest(string Firstname,
    string Lastname,
    DateTime DateOfBirth,
    string PhoneNumber,
    string Email,
    string BankAccountNumber);