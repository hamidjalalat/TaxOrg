using AutoMapper.QueryableExtensions;
using AutoMapper;
using MediatR;
using ViewModels.Menus;
using NazmMapping.Mappings;
using Nazm.Results;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using Application.Common;
using static Application.Common.GridHelper;
using System.Linq.Expressions;
using ViewModels.Shared;
using Application.Common.Extensions;

namespace Application.Features.Anemic.Menus.Queries
{

    public class MenuGetChildQuery : BaseRequest, IRequest<Result<PaginatedList<MenuViewModel>>>
    {
        private MenuGetChildQuery()
        {

        }
        public MenuGetChildQuery(PublicViewModel viewModel)
        {
            InputViewModel = viewModel;
        }
        public PublicViewModel InputViewModel { get; private set; }
    }

    public class MenuGetChildQueryHandler : BaseRequestHandler<MenuGetChildQuery, Result<PaginatedList<MenuViewModel>>>
    {
        private readonly IMenuRepository _MenuRepository;


        public MenuGetChildQueryHandler(
            IMenuRepository MenuRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _MenuRepository = MenuRepository;
        }

        protected async override Task<Result<PaginatedList<MenuViewModel>>> HandleRequestAsync(MenuGetChildQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<PaginatedList<MenuViewModel>>();
            var query = await _unitOfWork.Menus.GetChildList(input.InputViewModel.MenuId.Value, cancellationToken);
            var viewModel = query.ProjectTo<MenuViewModel>(_mapper.ConfigurationProvider, cancellationToken);

            if (input.InputViewModel.FilterParams != null && input.InputViewModel.FilterParams.SortBy != null)
            {
                viewModel = viewModel.OrderBy(input.InputViewModel  .FilterParams?.SortBy ?? "");
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
                Expression<Func<MenuViewModel, bool>> delegateQuery = ExpressionBuilder.GetExpression<MenuViewModel>(filters);
                var response = await viewModel
                  .Where(delegateQuery)
                  .PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);
                return result.WithValue(response).ConvertToDtatResult();
            }
            else
            {
                var menuViewModel = await viewModel
                     .PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);
                return result.WithValue(menuViewModel).ConvertToDtatResult();
            }
        }
    }
}
