using FluentValidation;
using ViewModels;

namespace ViewModels.MenuControllerActions;
public class MenuControllerActionUpdateViewModelValidator : AbstractValidator<MenuControllerActionUpdateViewModel>
{
    public MenuControllerActionUpdateViewModelValidator()
    {

        RuleFor(v => v.MenuControllerId)

            .NotEmpty()
            .WithName(Resources.DataDictionary.Id)
            .WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName));

        RuleFor(v => v.ActionMethodId)

            .NotEmpty()
            .WithName(Resources.DataDictionary.ActionMethodId)
            .WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName));
    }
}
