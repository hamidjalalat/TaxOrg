using FluentValidation;

namespace ViewModels.Users
{
    public class UserManageChangePasswordViewModelValidator : AbstractValidator<UserManageChangePasswordViewModel>
    {
        public UserManageChangePasswordViewModelValidator()
        {
            RuleFor(v => v.CurrentPassword)
                .NotEmpty()
                .WithName(Resources.DataDictionary.OldPassword)
                .WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
                ;

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
