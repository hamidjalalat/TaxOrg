using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Nazm_tspagents
{
    public class Nazm_tspagentInputDateInvoiceViewModelValidator : AbstractValidator<Nazm_tspagentInputDateInvoiceViewModel>
    {
        public Nazm_tspagentInputDateInvoiceViewModelValidator()
        {
            //RuleFor(Y => Y.FromDate)
            //          .GreaterThan(2023).WithMessage(string.Format(Resources.Messages.Validations.GreaterThan_FieldValue, ConstClass.PropertyName, 2023))
            //          .LessThan(2099).WithMessage(string.Format(Resources.Messages.Validations.LessThan_FieldValue, ConstClass.PropertyName, 2099))
            //          .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
            //          .WithName(Resources.DataDictionary.Year);
        }
    }
}
