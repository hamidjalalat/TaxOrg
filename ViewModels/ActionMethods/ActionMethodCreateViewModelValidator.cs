using FluentValidation;
using ViewModels;

namespace ViewModels.ActionMethods;
public class ActionMethodCreateViewModelValidator : AbstractValidator<ActionMethodCreateViewModel>
{
    public ActionMethodCreateViewModelValidator()
    {
        RuleFor(v => v.TitleFa)
            .MaximumLength(200).WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName, 200))
            .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
            .WithName(Resources.DataDictionary.TitleFa)
            .WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName));


        RuleFor(v => v.TitleEn)
           .MaximumLength(200).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 200))
           .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
            .WithName(Resources.DataDictionary.Title);


    }
}
