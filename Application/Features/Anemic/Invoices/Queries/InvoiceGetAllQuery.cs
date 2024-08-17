using Application.Common;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;
using NazmMapping.Mappings;
using System.Linq.Expressions;
using ViewModels.Invoices;
using ViewModels.Shared;
using static Application.Common.GridHelper;

namespace Application.Features.Anemic.Invoices.Queries
{

    public class InvoiceGetAllQuery : BaseRequest, IRequest<Result<IList<InvoiceViewModel>>>
    {
        private InvoiceGetAllQuery()
        {

        }
        public InvoiceGetAllQuery(PublicViewModel viewModel)
        {
            InputViewModel = viewModel;
        }

        public PublicViewModel InputViewModel { get; private set; }

    }

    public class InvoiceGetAllQueryHandler : BaseRequestHandler<InvoiceGetAllQuery, Result<IList<InvoiceViewModel>>>
    {

        public InvoiceGetAllQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, InvoiceService InvoiceService) : base(unitOfWork, mapper)
        {
            _InvoiceService = InvoiceService;
        }
        private readonly InvoiceService _InvoiceService;


        protected async override Task<Result<IList<InvoiceViewModel>>> HandleRequestAsync(InvoiceGetAllQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<IList<InvoiceViewModel>>();

            var query = await _InvoiceService.GetAsync();

            return result.WithValue(query).ConvertToDtatResult();

        }
    }
}
