using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ViewModels.RegulationGroups
{
    public class RegulationGroupActiveViewModelValidator : AbstractValidator<RegulationGroupActiveViewModel>
    {
        public RegulationGroupActiveViewModelValidator()
        {
            RuleFor(v => v.Title)
                 .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
                 .MaximumLength(50).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 50))
                 .WithName(Resources.DataDictionary.Title);

        }
    }
}
