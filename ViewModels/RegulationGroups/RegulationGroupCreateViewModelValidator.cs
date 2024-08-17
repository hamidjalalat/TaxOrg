using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace ViewModels.RegulationGroups
{
    public class RegulationGroupCreateViewModelValidator : AbstractValidator<RegulationGroupCreateViewModel>
    {
        public RegulationGroupCreateViewModelValidator()
        {
            RuleFor(v => v.Title)
                  .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
                  .MaximumLength(50).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 50))
                  .WithName(Resources.DataDictionary.Title);

            RuleFor(v => v.Code)
                  .GreaterThan(0).WithMessage(string.Format(Resources.Messages.Validations.GreaterThan_FieldValue, ConstClass.PropertyName, 0))
                  .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
                  .WithName(Resources.DataDictionary.Code);

            RuleFor(v => v.Sort)
                  .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
                  .WithName(Resources.DataDictionary.Sort);


        }
    }
}
