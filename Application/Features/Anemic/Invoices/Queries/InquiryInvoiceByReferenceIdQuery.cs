using Application.Common;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using AutoMapper;
using MediatR;
using Nazm.Results;
using ViewModels.Invoices;
using ViewModels.Nazm_tspagents;

namespace Application.Features.Anemic.Nazm_tspagents.Queries
{

    public class InquiryInvoiceByReferenceIdQuery : BaseRequest, IRequest<Result<InquiryViewModel>>
    {
        public InquiryInvoiceByReferenceIdQuery()
        {

        }
        public InquiryInvoiceByReferenceIdQuery(int reference_Id)
        {
            Reference_Id = reference_Id;
        }
        public int Reference_Id { get; set; }


    }

    public class InquiryInvoiceByGetByReferenceIdQueryHandler : BaseRequestHandler<InquiryInvoiceByReferenceIdQuery, Result<InquiryViewModel>>
    {
        private readonly INazm_tspagentRepository _Nazm_tspagentRepository;


        public InquiryInvoiceByGetByReferenceIdQueryHandler(InvoiceService InvoiceService, IMapper mapper, IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _InvoiceService = InvoiceService;
        }

        private readonly InvoiceService _InvoiceService;

        protected async override Task<Result<InquiryViewModel>> HandleRequestAsync(InquiryInvoiceByReferenceIdQuery input, CancellationToken cancellationToken)
        {

            var result = new FluentResults.Result<InquiryViewModel>();

            var query = await _InvoiceService.GetAsync();

            return result.WithValue(query).ConvertToDtatResult();



        }
    }
}
