using FluentValidation;
using ViewModels;

namespace ViewModels.MenuControllers;
public class MenuControllerUpdateViewModelValidator : AbstractValidator<MenuControllerUpdateViewModel>
{
    public MenuControllerUpdateViewModelValidator()
    {

        RuleFor(v => v.MenuId)

            .GreaterThan(0)
            .WithName(Resources.DataDictionary.MenuId)
            .WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName));

        RuleFor(v => v.ControllerId)

            .GreaterThan(0)
            .WithName(Resources.DataDictionary.ControllerId)
            .WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName));
    }
}
