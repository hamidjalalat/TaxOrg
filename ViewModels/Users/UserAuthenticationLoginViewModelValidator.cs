using FluentValidation;

namespace ViewModels.Users
{
    public class UserAuthenticationLoginViewModelValidator : AbstractValidator<UserAuthenticationLoginViewModel>
    {
        public UserAuthenticationLoginViewModelValidator()
        {
            RuleFor(v => v.UserName)
            .NotEmpty()
            .WithName(Resources.DataDictionary.UserName)
                    .WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName));

            RuleFor(v => v.Password)
            .NotEmpty()
            .WithName(Resources.DataDictionary.Password)
                    .WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName));

        }
    }
}
