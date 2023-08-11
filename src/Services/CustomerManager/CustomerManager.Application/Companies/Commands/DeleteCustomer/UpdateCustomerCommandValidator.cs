using FluentValidation;

namespace CustomerManager.Application.Companies.Commands.DeleteCustomer;

public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerCommandValidator()
    {
        RuleFor(x => x.GivenEmail)
            .NotEmpty();
    }
}