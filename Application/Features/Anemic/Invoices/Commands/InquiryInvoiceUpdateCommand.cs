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
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Application.Features.Anemic.Invoices.Commands
{
    public class InquiryInvoiceUpdateCommand : BaseRequest, IRequest<Result<string>>
    {
        public InvoiceUpdateViewModel InvoiceViewModel { get; set; }

    }

    public class InquiryInvoiceUpdateCommandHandler : BaseRequestHandler<InquiryInvoiceUpdateCommand, Result<string>>
    {


        public InquiryInvoiceUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, InquiryService inquiryService) : base(unitOfWork, mapper)
        {
            _inquiryService = inquiryService;
        }

        private readonly InquiryService _inquiryService;

        protected override async Task<Result<string>> HandleRequestAsync(InquiryInvoiceUpdateCommand input, CancellationToken cancellationToken)
        {
            try
            {
                var result = new FluentResults.Result<string>();

                var listInqueryByReferenceId = await _unitOfWork.Nazm_tspagents.GetAllByFilterForInQuery(take: 100);

                if (listInqueryByReferenceId.Count == 0)
                    return result.WithError(Resources.DataDictionary.ErrorInvoice).ConvertToDtatResult();

                var response = await _inquiryService.PostAsync(listInqueryByReferenceId, input.InvoiceViewModel.Token, input.InvoiceViewModel.XOrgId);

                for (int i = 0; i < response.Count; i++)
                {
                    var nazm_tspagent = await _unitOfWork.Nazm_tspagents.FindByReferenceIdAsync(response[i].ReferenceNumber);

                    StringBuilder Messages = new StringBuilder();

                    foreach (var item in response[i].Data.Error)
                    {
                        Messages.AppendLine(item.Message);
                    }

                    nazm_tspagent.Status = response[i].Status;

                    nazm_tspagent.Error_Description = Messages.ToString().Length > 512 ? Messages.ToString().Substring(1, 512) : Messages.ToString();
                    nazm_tspagent.InqueryDate = Nazm.DateTime.Now;

                    Nazm_tspagent entity = new Nazm_tspagent()
                    {
                        id = nazm_tspagent.id,
                        Status = nazm_tspagent.Status,
                        Error_Description = Messages.ToString(),
                        InqueryDate = Nazm.DateTime.Now
                    };

                    _unitOfWork.Nazm_tspagents.UpdateSpecificField(entity, x => x.Status, x => x.Error_Description, x => x.InqueryDate);

                    await _unitOfWork.Commit(cancellationToken);
                }

                if (response == null)
                {
                    return result.WithError(Resources.DataDictionary.ErrorInvoice).ConvertToDtatResult();
                }

                return result.WithValue($"{response.Count()} rows affected").ConvertToDtatResult();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransaction(cancellationToken);

                throw;
            }

        }
    }
}
