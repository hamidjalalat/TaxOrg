using Application.Common;
using Application.Common.Extensions;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using Application.Common.Security;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;
using NazmMapping.Mappings;
using System.Linq.Expressions;
using ViewModels;
using ViewModels.Nazm_tspagents;
using ViewModels.Shared;
using static Application.Common.GridHelper;

namespace Application.Features.Anemic.Nazm_tspagents.Queries
{
    public class Nazm_tspagentGetCountInvoiceSuccessQuery : BaseRequest, IRequest<Result<long>>
    {
        private Nazm_tspagentGetCountInvoiceSuccessQuery()
        {

        }
        public Nazm_tspagentGetCountInvoiceSuccessQuery(Nazm_tspagentInputDateInvoiceViewModel viewModel)
        {
         
            InputViewModel = viewModel;
        }
       
        public Nazm_tspagentInputDateInvoiceViewModel InputViewModel { get; private set; }

    }

    public class Nazm_tspagentGetCountInvoiceSuccessQueryHandler : BaseRequestHandler<Nazm_tspagentGetCountInvoiceSuccessQuery, Result<long>>
    {
        private readonly INazm_tspagentRepository _Nazm_tspagentRepository;


        public Nazm_tspagentGetCountInvoiceSuccessQueryHandler(INazm_tspagentRepository Nazm_tspagentRepository,IMapper mapper,IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _Nazm_tspagentRepository = Nazm_tspagentRepository;
        }

        protected async override Task<Result<long>> HandleRequestAsync(Nazm_tspagentGetCountInvoiceSuccessQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<long>();

            var query = _Nazm_tspagentRepository.GetCountInvoiceSuccess(input.InputViewModel);

            return result.WithValue(query.Result).ConvertToDtatResult();

        }
    }
}
