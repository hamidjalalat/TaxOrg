using FluentValidation;
using ViewModels;

namespace ViewModels.MenuControllerActions;
public class MenuControllerActionCreateViewModelValidator : AbstractValidator<MenuControllerActionCreateViewModel>
{
    public MenuControllerActionCreateViewModelValidator()
    {
        RuleFor(v => v.MenuControllerActionId)

            .GreaterThan(0)
            .WithName(Resources.DataDictionary.Id)
            .WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName));

        RuleFor(v => v.ActionMethodId)

            .GreaterThan(0)
            .WithName(Resources.DataDictionary.ActionMethodId)
            .WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName));
    }
}
