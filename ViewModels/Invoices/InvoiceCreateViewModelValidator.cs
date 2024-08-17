using FluentResults;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace ViewModels.Invoices
{
    public class InvoiceCreateViewModelValidator : AbstractValidator<InvoiceCreateViewModel>
    {
        public InvoiceCreateViewModelValidator()
        {
            RuleFor(v => v.Token)
                .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
                .WithName("Sstid");

            RuleFor(v => v.XOrgId)
               .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
               .WithName("Vra");

           

     
        }
    }
}
