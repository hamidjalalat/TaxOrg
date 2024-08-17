using FluentValidation;
using ViewModels;

namespace ViewModels.Controllers;
public class UpdateControllerCommandValidator : AbstractValidator<ControllerUpdateViewModel>
{
    public UpdateControllerCommandValidator()
    {

        RuleFor(v => v.TitleFa)
     .MaximumLength(200).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 200))
     .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
     .WithName(Resources.DataDictionary.TitleFa);


        RuleFor(v => v.TitleEn)
            .MaximumLength(200).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 200))
            .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
            .WithName(Resources.DataDictionary.Title);

    }
}
