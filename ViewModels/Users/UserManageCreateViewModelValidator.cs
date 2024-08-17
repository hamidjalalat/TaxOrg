using FluentValidation;

namespace ViewModels.Users
{
   
    public class UserManageCreateViewModelValidator : AbstractValidator<UserManageCreateViewModel>
    {
        public UserManageCreateViewModelValidator()
        {

            RuleFor(v => v.UserName)
                .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
                .WithName(Resources.DataDictionary.UserName);

            RuleFor(v => v.FirstName)
                .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
                .WithName(Resources.DataDictionary.FirstName);

            RuleFor(v => v.LastName)
               .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
               .WithName(Resources.DataDictionary.LastName);

            RuleFor(v => v.Password)
               .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
               .WithName(Resources.DataDictionary.Password);

            RuleFor(v => v.ConfirmPassword)

               .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
               .WithName(Resources.DataDictionary.ConfirmNewPassword)
               .Equal(customer => customer.ConfirmPassword).WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName));


        }
    }
}
