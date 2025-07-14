
using FluentValidation;
using Multiple_Desk.Models.Entities;

public class MessageModelValidators : AbstractValidator<MessagesModel>
{

    public MessageModelValidators()
    {
        RuleFor(x=>x.FirstName)
            .NotEmpty().WithMessage("First Name is required")
            .MaximumLength(50).WithMessage("First Name cannot exceed 50 characters");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last Name is required")
            .MaximumLength(50).WithMessage("Last Name cannot exceed 50 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
          //  .EmailAddress().WithMessage("Invalid email format.")
            .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z]+\.[a-zA-Z]{2,}$")
                   .WithMessage("Please enter a valid email address.")
            .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");

        RuleFor(x => x.Company)
            .MaximumLength(100).WithMessage("Company name cannot exceed 100 characters");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone Number is required")
            .Matches(@"^\+?[0-9\s\-()]+$").WithMessage("Invalid phone number format")
            .MaximumLength(10).WithMessage("Phone Number cannot exceed 10 characters")
            .MinimumLength(10).WithMessage("Phone Number must be 10 characters long");
    }
   
}

