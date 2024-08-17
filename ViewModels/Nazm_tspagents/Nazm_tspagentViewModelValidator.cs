using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Nazm_tspagents
{
    public class Nazm_tspagentViewModelValidator : AbstractValidator<Nazm_tspagentViewModel>
    {
        public Nazm_tspagentViewModelValidator()
        {
            //RuleFor(v => v.Id)
            //    .NotEmpty()
            //    .WithName(Resources.DataDictionary.Id)
            //    .WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName));


        }
    }
}
