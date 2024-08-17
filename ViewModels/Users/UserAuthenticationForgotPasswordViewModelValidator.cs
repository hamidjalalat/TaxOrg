
using FluentValidation;

namespace ViewModels.Users
{
    public class UserAuthenticationForgotPasswordViewModelValidator : AbstractValidator<UserAuthenticationForgotPasswordViewModel>
    {
        public UserAuthenticationForgotPasswordViewModelValidator()
        {
            RuleFor(v => v.Email)
            .NotEmpty()
            .WithName(Resources.DataDictionary.EmailAddress)
                    .WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName));

            
        }
    }
}
