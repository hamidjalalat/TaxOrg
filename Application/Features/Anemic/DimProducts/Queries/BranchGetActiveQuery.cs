using AutoMapper;
using Domain.Constants;
using MediatR;
using NazmMapping.Mappings;
using ViewModels.DimProducts;
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

namespace Application.Features.Anemic.DimProducts.Queries
{

    public class DimProductGetActiveQuery : BaseRequest, IRequest<Result<PaginatedList<DimProductActiveViewModel>>>
    {
        private DimProductGetActiveQuery()
        {

        }
        public DimProductGetActiveQuery(PublicViewModel viewModel)
        {
            InputViewModel = viewModel;
        }
        public PublicViewModel InputViewModel { get; private set; }
    }

    public class DimProductGetActiveQueryHandler : BaseRequestHandler<DimProductGetActiveQuery, Result<PaginatedList<DimProductActiveViewModel>>>
    {
        private readonly IDimProductRepository _DimProductRepository;

        public DimProductGetActiveQueryHandler(IDimProductRepository DimProductRepository,IMapper mapper,IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _DimProductRepository = DimProductRepository;
        }

        protected async override Task<Result<PaginatedList<DimProductActiveViewModel>>> HandleRequestAsync(DimProductGetActiveQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<PaginatedList<DimProductActiveViewModel>>();

            var query = _unitOfWork.DimProducts.GetAll.Where(s => s.IsActive).OrderBy(s => s.Sort);

            var viewModel = query.ProjectTo<DimProductActiveViewModel>(_mapper.ConfigurationProvider, cancellationToken);

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

                Expression<Func<DimProductActiveViewModel, bool>> delegateQuery = ExpressionBuilder.GetExpression<DimProductActiveViewModel>(filters);

                var response = await viewModel.Where(delegateQuery).PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);

                return result.WithValue(response).ConvertToDtatResult();
            }
            else
            {
                var DimProductActiveViewModel = await viewModel.PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);

                return result.WithValue(DimProductActiveViewModel).ConvertToDtatResult();
            }
           
        }
    }
}
