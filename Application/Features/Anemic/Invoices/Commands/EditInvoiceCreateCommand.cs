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
using Domain.Rich.SharedKernel;
using DocumentFormat.OpenXml.Presentation;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;

namespace Application.Features.Anemic.Invoices.Commands
{
    public class EditInvoiceCreateCommand : BaseRequest, IRequest<Result<List<InvoiceViewModel>>>
    {
        public InvoiceCreateViewModel InvoiceViewModel { get; set; }

    }

    public class EditInvoiceCreateCommandHandler : BaseRequestHandler<EditInvoiceCreateCommand, Result<List<InvoiceViewModel>>>
    {


        public EditInvoiceCreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, InvoiceService InvoiceService) : base(unitOfWork, mapper)
        {
            _InvoiceService = InvoiceService;

        }
        private readonly InvoiceService _InvoiceService;

        protected override async Task<Result<List<InvoiceViewModel>>> HandleRequestAsync(EditInvoiceCreateCommand input, CancellationToken cancellationToken)
        {
            try
            {
                var result = new FluentResults.Result<List<InvoiceViewModel>>();

                var listNazm_tspagents = await _unitOfWork.Nazm_tspagents.GetAllByFilterForEditInvoice(100);

                if (listNazm_tspagents.Count == 0)
                    return result.WithError(Resources.DataDictionary.ErrorInvoice).ConvertToDtatResult();

                ToObjectConversion objectSet = new ToObjectConversion();

                var listInvoiceViewModel = objectSet.GetCancelInvoiceSetToObject(listNazm_tspagents);

                var response = await _InvoiceService.PostAsync(listInvoiceViewModel, input.InvoiceViewModel.Token, input.InvoiceViewModel.XOrgId);

                int RecourdCount = response.Count;

                for (int i = 0; i < RecourdCount; i++)
                {
                    var nazm_tspagent = await _unitOfWork.Nazm_tspagents.FindByInnoForEditInvoiceAsync(response[i].Inno);
                    
                    nazm_tspagent.Reference_Id = response[i].ReferenceNumber;

                    Nazm_tspagent entity = new Nazm_tspagent()
                    {
                        id = nazm_tspagent.id,
                        Reference_Id = nazm_tspagent.Reference_Id,
                        Status=null,
                        InqueryDate=null
                    };

                    _unitOfWork.Nazm_tspagents.UpdateSpecificField(entity, x => x.Reference_Id,x=>x.Status,x=>x.InqueryDate);

                    await _unitOfWork.Commit(cancellationToken);
                }

                if (response == null)
                {
                    return result.WithError(Resources.DataDictionary.ErrorInvoice).ConvertToDtatResult();
                }

                return result.WithValue(response).ConvertToDtatResult();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransaction(cancellationToken);

                throw;
            }

        }


    }
}
