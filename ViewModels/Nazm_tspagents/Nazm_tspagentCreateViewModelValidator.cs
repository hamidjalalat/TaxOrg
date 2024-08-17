using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace ViewModels.Nazm_tspagents
{
    public class Nazm_tspagentCreateViewModelValidator : AbstractValidator<Nazm_tspagentCreateViewModel>
    {
        public Nazm_tspagentCreateViewModelValidator()
        {
            RuleFor(I => I.indatim.ToString())
                  .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
                  .MaximumLength(23).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 23))
                  .WithName(Resources.DataDictionary.indatim);

            RuleFor(I => I.inno)
                   .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
                   .WithName(Resources.DataDictionary.inno);

            RuleFor(I => I.vra)
                   .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
                   .WithName(Resources.Messages.Validations.Vra);

            RuleFor(I => I.tins)
                   .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
                   .WithName(Resources.Messages.Validations.Tins);

            RuleFor(I => I.sstt)
                   .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
                   .MaximumLength(400).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 400))
                   .WithName(Resources.Messages.Validations.Sstt);

            RuleFor(I => I.sstid)
                   .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
                   .MaximumLength(13).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 13))
                   .WithName(Resources.Messages.Validations.Sstid);

            RuleFor(I => I.ins)
                   .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
                   .WithName(Resources.Messages.Validations.Ins);

            RuleFor(I => I.bid)
                   .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
                   .WithName(Resources.Messages.Validations.Bid);

            RuleFor(I => I.dis)
                  .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
                  .WithName(Resources.Messages.Validations.Dis);

            RuleFor(I => I.bpc)
                  .NotEmpty().WithMessage(string.Format(Resources.Messages.Validations.Required, ConstClass.PropertyName))
                  .MaximumLength(10).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 10))
                  .WithName(Resources.Messages.Validations.Bpc);

            RuleFor(I => I.acn)
                 .MaximumLength(14).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 14))
                 .WithName(Resources.Messages.Validations.Acn);

            RuleFor(I => I.bpn)
                 .MaximumLength(9).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 9))
                 .WithName(Resources.Messages.Validations.Bpn);

            RuleFor(I => I.acn)
                .MaximumLength(9).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 9))
                .WithName(Resources.Messages.Validations.Acn);

            RuleFor(I => I.bsrn)
                .MaximumLength(12).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 12))
                .WithName(Resources.Messages.Validations.Bsrn);

            RuleFor(I => I.consfee)
                 .MaximumLength(18).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 18))
                 .WithName(Resources.Messages.Validations.Consfee);

            RuleFor(I => I.cop)
                 .MaximumLength(18).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 18))
                 .WithName(Resources.Messages.Validations.Cop);

            RuleFor(I => I.crn)
                 .MaximumLength(12).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 12))
                 .WithName(Resources.Messages.Validations.Crn);

            RuleFor(I => I.cut)
                 .MaximumLength(3).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 3))
                 .WithName(Resources.Messages.Validations.Cut);

            RuleFor(I => I.bbc)
           .MaximumLength(4).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 4))
           .WithName(Resources.Messages.Validations.Bbc);

            RuleFor(I => I.iinn)
                .MaximumLength(9).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 9))
                .WithName(Resources.Messages.Validations.Iinn);

            RuleFor(I => I.irtaxid)
                .MaximumLength(22).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 22))
                .WithName(Resources.Messages.Validations.Irtaxid);

            RuleFor(I => I.irtaxid)
                .MaximumLength(22).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 22))
                .WithName(Resources.Messages.Validations.Irtaxid);

            RuleFor(I => I.odam)
                 .MaximumLength(18).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 18))
                 .WithName(Resources.Messages.Validations.Odam);

            RuleFor(I => I.odr)
                 .MaximumLength(225).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 225))
                 .WithName(Resources.Messages.Validations.Odr);

            RuleFor(I => I.odt)
                  .MaximumLength(225).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 225))
                  .WithName(Resources.Messages.Validations.Odt);

            RuleFor(I => I.olt)
                  .MaximumLength(225).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 225))
                  .WithName(Resources.Messages.Validations.Olt);
                  
            RuleFor(I => I.pcn)
                  .MaximumLength(16).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 16))
                  .WithName(Resources.Messages.Validations.Pcn);
                  
            RuleFor(I => I.pdt)
                  .MaximumLength(13).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 13))
                  .WithName(Resources.Messages.Validations.Pdt);
                  
            RuleFor(I => I.pid)
                  .MaximumLength(12).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 12))
                  .WithName(Resources.Messages.Validations.Pid);
                  
            RuleFor(I => I.pmt)
                  .MaximumLength(2).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 2))
                  .WithName(Resources.Messages.Validations.Pmt);
                  
            RuleFor(I => I.sbc)
                  .MaximumLength(2).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 2))
                  .WithName(Resources.Messages.Validations.Sbc);
                  
             RuleFor(I => I.scc)
                  .MaximumLength(5).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 5))
                  .WithName(Resources.Messages.Validations.Scc);
                  
             RuleFor(I => I.scln)
                  .MaximumLength(14).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 14))
                  .WithName(Resources.Messages.Validations.Scln);
                  
              RuleFor(I => I.trmn)
                  .MaximumLength(8).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 8))
                  .WithName(Resources.Messages.Validations.Trmn);
                  
              RuleFor(I => I.tins)
                  .MaximumLength(14).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 14))
                  .WithName(Resources.Messages.Validations.Tins);
                  
              RuleFor(I => I.Address)
                  .MaximumLength(200).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 200))
                  .WithName(Resources.Messages.Validations.Address);
                  
              RuleFor(I => I.Policy_No)
                  .MaximumLength(100).WithMessage(string.Format(Resources.Messages.Validations.MaxLength, ConstClass.PropertyName, 100))
                  .WithName(Resources.Messages.Validations.Address);
        }
    }
}
