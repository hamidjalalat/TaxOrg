using AutoMapper;
using Domain.Constants;
using MediatR;
using NazmMapping.Mappings;
using ViewModels.Netsales;
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

namespace Application.Features.Anemic.Netsales.Queries
{

    public class NetsaleGetActiveQuery : BaseRequest, IRequest<Result<PaginatedList<NetsaleActiveViewModel>>>
    {
        private NetsaleGetActiveQuery()
        {

        }
        public NetsaleGetActiveQuery(PublicViewModel viewModel)
        {
            InputViewModel = viewModel;
        }
        public PublicViewModel InputViewModel { get; private set; }
    }

    public class NetsaleGetActiveQueryHandler : BaseRequestHandler<NetsaleGetActiveQuery, Result<PaginatedList<NetsaleActiveViewModel>>>
    {
        private readonly INetsaleRepository _NetsaleRepository;

        public NetsaleGetActiveQueryHandler(INetsaleRepository NetsaleRepository,IMapper mapper,IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _NetsaleRepository = NetsaleRepository;
        }

        protected async override Task<Result<PaginatedList<NetsaleActiveViewModel>>> HandleRequestAsync(NetsaleGetActiveQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<PaginatedList<NetsaleActiveViewModel>>();

            var query = _unitOfWork.Netsales.GetAll.Where(s => s.IsActive).OrderBy(s => s.Sort);

            var viewModel = query.ProjectTo<NetsaleActiveViewModel>(_mapper.ConfigurationProvider, cancellationToken);

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

                Expression<Func<NetsaleActiveViewModel, bool>> delegateQuery = ExpressionBuilder.GetExpression<NetsaleActiveViewModel>(filters);

                var response = await viewModel.Where(delegateQuery).PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);

                return result.WithValue(response).ConvertToDtatResult();
            }
            else
            {
                var NetsaleActiveViewModel = await viewModel.PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);

                return result.WithValue(NetsaleActiveViewModel).ConvertToDtatResult();
            }
           
        }
    }
}
