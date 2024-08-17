using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Nazm.Results;
using ViewModels.Invoices;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using Application.Common;
using Microsoft.EntityFrameworkCore;
using ViewModels.Nazm_tspagents;
using System.Text;
using System;

namespace Application.Features.Anemic.Invoices.Commands
{
    public class UpdateDateCommand : BaseRequest, IRequest<Result<List<InvoiceViewModel>>>
    {
        public UpdateDateViewModel InvoiceViewModel { get; set; }

    }

    public class UpdateDateCommandHandler : BaseRequestHandler<UpdateDateCommand, Result<List<InvoiceViewModel>>>
    {
        public UpdateDateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _func = ConditionDate;
            _reduceNow = System.DateTime.Now.AddDays(-21).Date;
        }

        private System.DateTime _reduceNow;

        private Func<Nazm_tspagent, bool> _func;

        protected override async Task<Result<List<InvoiceViewModel>>> HandleRequestAsync(UpdateDateCommand input, CancellationToken cancellationToken)
        {
            try
            {
                var result = new FluentResults.Result<List<InvoiceViewModel>>();

                var listNazm_tspagents = _unitOfWork.Nazm_tspagents.GetAll
                  .Where(T => T.TaxId == null)
                  .Where(R => R.Reference_Id == null)
                  .Where(ConditionDate)
                  .ToList();

                for (int i = 0; i < listNazm_tspagents.Count; i++)
                {
                    listNazm_tspagents[i].indatim= System.DateTime.Now.AddDays(-3).Date;
                    _unitOfWork.Nazm_tspagents.Update(listNazm_tspagents[i]);

                    await _unitOfWork.Commit(cancellationToken);
                }

                return result.WithValue(result.Value).ConvertToDtatResult();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransaction(cancellationToken);

                throw;
            }

        }
        public bool ConditionDate(Nazm_tspagent tspagent)
        {
            var covertDate = Convert.ToDateTime(tspagent.indatim);

            if (covertDate < _reduceNow)
            {
                return true;
            }
            else
            {
             
                return false;

            }
        }
    }
}
