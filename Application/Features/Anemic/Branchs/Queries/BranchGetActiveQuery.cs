using AutoMapper;
using Domain.Constants;
using MediatR;
using NazmMapping.Mappings;
using ViewModels.Branchs;
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

namespace Application.Features.Anemic.Branchs.Queries
{

    public class BranchGetActiveQuery : BaseRequest, IRequest<Result<PaginatedList<BranchActiveViewModel>>>
    {
        private BranchGetActiveQuery()
        {

        }
        public BranchGetActiveQuery(PublicViewModel viewModel)
        {
            InputViewModel = viewModel;
        }
        public PublicViewModel InputViewModel { get; private set; }
    }

    public class BranchGetActiveQueryHandler : BaseRequestHandler<BranchGetActiveQuery, Result<PaginatedList<BranchActiveViewModel>>>
    {
        private readonly IBranchRepository _BranchRepository;

        public BranchGetActiveQueryHandler(IBranchRepository BranchRepository,IMapper mapper,IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _BranchRepository = BranchRepository;
        }

        protected async override Task<Result<PaginatedList<BranchActiveViewModel>>> HandleRequestAsync(BranchGetActiveQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<PaginatedList<BranchActiveViewModel>>();

            var query = _unitOfWork.Branchs.GetAll.Where(s => s.IsActive).OrderBy(s => s.Sort);

            var viewModel = query.ProjectTo<BranchActiveViewModel>(_mapper.ConfigurationProvider, cancellationToken);

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

                Expression<Func<BranchActiveViewModel, bool>> delegateQuery = ExpressionBuilder.GetExpression<BranchActiveViewModel>(filters);

                var response = await viewModel.Where(delegateQuery).PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);

                return result.WithValue(response).ConvertToDtatResult();
            }
            else
            {
                var BranchActiveViewModel = await viewModel.PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);

                return result.WithValue(BranchActiveViewModel).ConvertToDtatResult();
            }
           
        }
    }
}
