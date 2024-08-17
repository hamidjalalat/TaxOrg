
using FluentValidation;

namespace ViewModels.Users
{
    public class UserManageChangePasswordByAdminViewModelValidator : AbstractValidator<UserManageChangePasswordByAdminViewModel>
    {
        public UserManageChangePasswordByAdminViewModelValidator()
        {
            RuleFor(v => v.NewPassword)
                .NotEmpty()
                .WithName(Resources.DataDictionary.NewPassword)
                .WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
                ;

            RuleFor(v => v.NewConfirmPassword)
                .NotEmpty()
                .WithName(Resources.DataDictionary.ConfirmNewPassword)
                .WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
                ;

            RuleFor(v => v.NewConfirmPassword)
                .Equal(x => x.NewPassword)
                .WithName(Resources.DataDictionary.ConfirmNewPassword)
                .WithMessage(string.Format(Resources.Messages.Validations.NotEqual, ConstClass.PropertyName))
                ;

        }
    }
}
