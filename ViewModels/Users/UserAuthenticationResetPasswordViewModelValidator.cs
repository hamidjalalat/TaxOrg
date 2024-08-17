using FluentValidation;

namespace ViewModels.Users
{
    public class UserAuthenticationResetPasswordViewModelValidator : AbstractValidator<UserAuthenticationResetPasswordViewModel>
    {
        public UserAuthenticationResetPasswordViewModelValidator()
        {
            RuleFor(v => v.Email)
            .NotEmpty()
            .WithName(Resources.DataDictionary.EmailAddress)
                    .WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName));

            RuleFor(v => v.Token)
            .NotEmpty()
            .WithName(Resources.DataDictionary.Token)
                    .WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName));

            RuleFor(v => v.NewPassword)
            .NotEmpty()
            .WithName(Resources.DataDictionary.NewPassword)
                    .WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName));

            RuleFor(v => v.ConfirmNewPassword)
            .NotEmpty()
            .WithName(Resources.DataDictionary.ConfirmNewPassword)
                    .WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName));


            RuleFor(v => v.ConfirmNewPassword)
            .Equal(x => x.NewPassword)
            .WithName(Resources.DataDictionary.ConfirmNewPassword)
                    .WithMessage(string.Format(Resources.Messages.Validations.NotEqual, ConstClass.PropertyName));

        }
    }
}
