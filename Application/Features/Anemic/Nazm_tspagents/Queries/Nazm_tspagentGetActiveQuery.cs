using AutoMapper;
using Domain.Constants;
using MediatR;
using NazmMapping.Mappings;
using ViewModels.Nazm_tspagents;
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

namespace Application.Features.Anemic.Nazm_tspagents.Queries
{

    public class Nazm_tspagentGetActiveQuery : BaseRequest, IRequest<Result<PaginatedList<Nazm_tspagentActiveViewModel>>>
    {
        private Nazm_tspagentGetActiveQuery()
        {

        }
        public Nazm_tspagentGetActiveQuery(PublicViewModel viewModel)
        {
            InputViewModel = viewModel;
        }
        public PublicViewModel InputViewModel { get; private set; }
    }

    public class Nazm_tspagentGetActiveQueryHandler : BaseRequestHandler<Nazm_tspagentGetActiveQuery, Result<PaginatedList<Nazm_tspagentActiveViewModel>>>
    {
        private readonly INazm_tspagentRepository _Nazm_tspagentRepository;


        public Nazm_tspagentGetActiveQueryHandler(
            INazm_tspagentRepository Nazm_tspagentRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _Nazm_tspagentRepository = Nazm_tspagentRepository;
        }

        protected async override Task<Result<PaginatedList<Nazm_tspagentActiveViewModel>>> HandleRequestAsync(Nazm_tspagentGetActiveQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<PaginatedList<Nazm_tspagentActiveViewModel>>();
            var query = _unitOfWork.Nazm_tspagents.GetAll
                .Where(s => s.IsActive)
                .OrderBy(s => s.Sort);
            var viewModel = query.ProjectTo<Nazm_tspagentActiveViewModel>(_mapper.ConfigurationProvider, cancellationToken);

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
                Expression<Func<Nazm_tspagentActiveViewModel, bool>> delegateQuery = ExpressionBuilder.GetExpression<Nazm_tspagentActiveViewModel>(filters);
                var response = await viewModel
                  .Where(delegateQuery)
                  .PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);
                return result.WithValue(response).ConvertToDtatResult();
            }
            else
            {
                var Nazm_tspagentActiveViewModel = await viewModel
                    .PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);

                return result.WithValue(Nazm_tspagentActiveViewModel).ConvertToDtatResult();
            }
           
        }
    }
}
