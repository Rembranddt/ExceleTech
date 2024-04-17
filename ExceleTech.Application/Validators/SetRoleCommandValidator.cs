
using ExceleTech.Application.Commands.AccountCommands.SetRoleCommand;
using ExceleTech.Domain.Enums;
using ExceleTech.Domain.ValueObjects;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceleTech.Application.Validators
{
    public class SetRoleCommandValidator : AbstractValidator<SetRoleCommand>
    {
        public SetRoleCommandValidator() 
        {
            RuleFor(s => s.SetRoleDTO.RoleName)
                .NotEmpty()
                .WithMessage("RoleName shoudn't be empty")

                .Must(BeAUserRole)
                .WithMessage("Role is Undefined");

            RuleFor(s => s.SetRoleDTO.UserId)
                .NotEmpty()
                .WithMessage("UserId shoudn't be empty");

                
        }
        private bool BeAUserRole(string roleName)
        {
            bool result = Enum.TryParse<UserRole>(roleName, out UserRole ya);
            return result;
        }
    }
}
