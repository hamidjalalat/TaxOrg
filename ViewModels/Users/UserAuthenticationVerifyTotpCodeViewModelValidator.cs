using FluentValidation;

namespace ViewModels.Users
{
    public class UserAuthenticationVerifyTotpCodeViewModelValidator : AbstractValidator<UserAuthenticationVerifyTotpCodeViewModel>
    {
        public UserAuthenticationVerifyTotpCodeViewModelValidator()
        {
            RuleFor(v => v.TotpCode)
            .NotEmpty()
            .WithName(Resources.DataDictionary.TotpCode)
                    .WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName));

            RuleFor(v => v.UserId)
            .NotEmpty()
            .WithName(Resources.DataDictionary.UserId)
                    .WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName));

            
        }
    }
}
