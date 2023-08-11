using FluentValidation;

namespace CustomerManager.Application.Companies.Commands.AddNewCustomer;

public class AddNewCustomerCommandValidator : AbstractValidator<AddNewCustomerCommand>
{
    public AddNewCustomerCommandValidator()
    {
        RuleFor(x => x.Firstname)
            .NotEmpty();

        RuleFor(x => x.Lastname)
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty();

        RuleFor(x => x.PhoneNumber)
            .NotEmpty();

        RuleFor(x => x.DateOfBirth)
            .NotEmpty();

        RuleFor(x => x.BankAccountNumber)
            .NotEmpty();
    }
}