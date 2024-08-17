using Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Shared;

namespace ViewModels.Nazm_tspagents
{
    public class Nazm_tspagentInputParamsViewModelValidator : AbstractValidator<Nazm_tspagentInputParamsViewModel>
    {
        public Nazm_tspagentInputParamsViewModelValidator()
        {
            //RuleFor(Y => Y.Inno)
            //          .GreaterThan(0).WithMessage(string.Format(Resources.Messages.Validations.GreaterThan_FieldValue, ConstClass.PropertyName, 0))
            //          .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
            //          .WithName(Resources.DataDictionary.inno);

            //RuleFor(v => v.Bid)
            //   .Matches(Domain.Constants.ValidationConstant.NationalCodePattern)
            //   .WithName(Resources.DataDictionary.NationalCode)
            //   .WithMessage(string.Format(Resources.Messages.Validations.InvalidValue, ConstClass.PropertyName))
            //   ;
        }

    }
}
