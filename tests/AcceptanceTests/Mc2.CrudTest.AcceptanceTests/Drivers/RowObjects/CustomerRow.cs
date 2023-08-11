namespace Mc2.CrudTest.AcceptanceTests.Drivers.RowObjects;

public record CustomerRow(string Firstname,
    string Lastname,
    DateTime DateOfBirth,
    string PhoneNumber,
    string Email,
    string BankAccountNumber);