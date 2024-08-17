using AutoMapper;
using Domain.Constants;
using MediatR;
using NazmMapping.Mappings;
using ViewModels.InvoiceModels;
using Nazm.Results;
using Application.Common.Extensions;
using AutoMapper.QueryableExtensions;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using ViewModels.Shared;
using ViewModels;
using Application.Common;
using System.Linq.Expressions;
using static Application.Common.GridHelper;

namespace Application.Features.Anemic.InvoiceModels.Queries
{

    public class InvoiceModelGetActiveQuery : BaseRequest, IRequest<Result<PaginatedList<InvoiceModelActiveViewModel>>>
    {
        private InvoiceModelGetActiveQuery()
        {

        }
        public InvoiceModelGetActiveQuery(PublicViewModel viewModel)
        {
            InputViewModel = viewModel;
        }
        public PublicViewModel InputViewModel { get; private set; }
    }

    public class InvoiceModelGetActiveQueryHandler : BaseRequestHandler<InvoiceModelGetActiveQuery, Result<PaginatedList<InvoiceModelActiveViewModel>>>
    {
        private readonly IInvoiceModelRepository _InvoiceModelRepository;

        public InvoiceModelGetActiveQueryHandler(IInvoiceModelRepository InvoiceModelRepository,IMapper mapper,IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _InvoiceModelRepository = InvoiceModelRepository;
        }

        protected async override Task<Result<PaginatedList<InvoiceModelActiveViewModel>>> HandleRequestAsync(InvoiceModelGetActiveQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<PaginatedList<InvoiceModelActiveViewModel>>();

            var query = _unitOfWork.InvoiceModels.GetAll.Where(s => s.IsActive).OrderBy(s => s.Sort);

            var viewModel = query.ProjectTo<InvoiceModelActiveViewModel>(_mapper.ConfigurationProvider, cancellationToken);

            if (input.InputViewModel.FilterParams != null && input.InputViewModel.FilterParams.SortBy != null)
            {
                viewModel = viewModel.OrderBy(input.InputViewModel.FilterParams?.SortBy ?? "");
            }

            if (input.InputViewModel.FilterParams != null && input.InputViewModel.FilterParams.Filter != null && input.InputViewModel.FilterParams.Filter.Count > 0)
            {
                var filters = new List<Filter>();

                foreach (var item in input.InputViewModel.FilterParams.Filter)
                {
                    filters.Add(new Filter()
                    {
                        Operator = item.Operator.ToLower().GetOperator(),
                        PropertyName = item.Field,
                        Value = item.Value,
                    });
                }

                Expression<Func<InvoiceModelActiveViewModel, bool>> delegateQuery = ExpressionBuilder.GetExpression<InvoiceModelActiveViewModel>(filters);

                var response = await viewModel.Where(delegateQuery).PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);

                return result.WithValue(response).ConvertToDtatResult();
            }
            else
            {
                var InvoiceModelActiveViewModel = await viewModel.PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);

                return result.WithValue(InvoiceModelActiveViewModel).ConvertToDtatResult();
            }
           
        }
    }
}
