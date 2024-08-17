using FluentValidation;

namespace ViewModels.Users
{
    public class UserAuthenticationSendTotpCodeViewModelValidator : AbstractValidator<UserAuthenticationSendTotpCodeViewModel>
    {
        public UserAuthenticationSendTotpCodeViewModelValidator()
        {
            RuleFor(v => v.PhoneNumber)
            .NotEmpty()
            .WithName(Resources.DataDictionary.CellPhoneNumber)
                    .WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName));

           
        }
    }
}
