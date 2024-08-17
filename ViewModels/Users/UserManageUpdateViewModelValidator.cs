using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Users
{
    
    public class UserManageUpdateViewModelValidator : AbstractValidator<UserManageUpdateViewModel>
    {
        public UserManageUpdateViewModelValidator()
        {

            
            RuleFor(v => v.FirstName)
                .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
                .WithName(Resources.DataDictionary.FirstName);

            RuleFor(v => v.LastName)
               .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
               .WithName(Resources.DataDictionary.LastName);

        }
    }
}
