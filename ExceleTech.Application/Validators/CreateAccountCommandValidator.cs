using ExceleTech.Application.Commands.AccountCommands.CreateAccountCommand;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceleTech.Application.Validators
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>

    {
        public CreateAccountCommandValidator()
        {
            
            RuleFor(command => command.CreateAccountDTO
            .email)
            .NotEmpty()
            .WithMessage("Email can't be empty")
            .EmailAddress()
            .WithMessage("Please enter a valid email")
            .MaximumLength(50)
            .WithMessage("Email's lenght can't be more than 50 symbols");


            RuleFor(command => command.CreateAccountDTO
            .password)
            .MinimumLength(9)
            .WithMessage("Password lenght should be more than 8")
            .Equal(command => command.CreateAccountDTO.confirmPassword)
            .WithMessage("Passwords must match");


            RuleFor(command => command.CreateAccountDTO.Name)
            .NotEmpty()
            .WithMessage("Name can't be empty")
            .MinimumLength(4)
            .WithMessage("Name lenght should be more than 3")
            .MaximumLength(50)
            .WithMessage("Name's lenght can't be more than 50 symbols");












        }
    }
}
