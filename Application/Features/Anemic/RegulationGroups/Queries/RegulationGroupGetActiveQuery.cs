using AutoMapper;
using Domain.Constants;
using MediatR;
using NazmMapping.Mappings;
using ViewModels.RegulationGroups;
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

namespace Application.Features.Anemic.RegulationGroups.Queries
{

    public class RegulationGroupGetActiveQuery : BaseRequest, IRequest<Result<PaginatedList<RegulationGroupActiveViewModel>>>
    {
        private RegulationGroupGetActiveQuery()
        {

        }
        public RegulationGroupGetActiveQuery(PublicViewModel viewModel)
        {
            InputViewModel = viewModel;
        }
        public PublicViewModel InputViewModel { get; private set; }
    }

    public class RegulationGroupGetActiveQueryHandler : BaseRequestHandler<RegulationGroupGetActiveQuery, Result<PaginatedList<RegulationGroupActiveViewModel>>>
    {
        private readonly IRegulationGroupRepository _RegulationGroupRepository;


        public RegulationGroupGetActiveQueryHandler(
            IRegulationGroupRepository RegulationGroupRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _RegulationGroupRepository = RegulationGroupRepository;
        }

        protected async override Task<Result<PaginatedList<RegulationGroupActiveViewModel>>> HandleRequestAsync(RegulationGroupGetActiveQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<PaginatedList<RegulationGroupActiveViewModel>>();
            var query = _unitOfWork.RegulationGroups.GetAll
                .Where(s => s.IsActive)
                .OrderBy(s => s.Sort);
            var viewModel = query.ProjectTo<RegulationGroupActiveViewModel>(_mapper.ConfigurationProvider, cancellationToken);

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
                Expression<Func<RegulationGroupActiveViewModel, bool>> delegateQuery = ExpressionBuilder.GetExpression<RegulationGroupActiveViewModel>(filters);
                var response = await viewModel
                  .Where(delegateQuery)
                  .PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);
                return result.WithValue(response).ConvertToDtatResult();
            }
            else
            {
                var RegulationGroupActiveViewModel = await viewModel
                    .PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);

                return result.WithValue(RegulationGroupActiveViewModel).ConvertToDtatResult();
            }
           
        }
    }
}
