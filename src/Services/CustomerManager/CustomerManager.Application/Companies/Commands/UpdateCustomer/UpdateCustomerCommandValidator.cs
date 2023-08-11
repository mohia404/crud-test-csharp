using FluentValidation;

namespace CustomerManager.Application.Companies.Commands.UpdateCustomer;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(x => x.GivenEmail)
            .NotEmpty();

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