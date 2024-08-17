using Application.Common.Extensions;
using Application.Common;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;
using NazmMapping.Mappings;
using System.Linq.Expressions;
using ViewModels.Menus;
using ViewModels.Shared;
using static Application.Common.GridHelper;

namespace Application.Features.Anemic.Menus.Queries
{
   
    public class MenuGetAllQuery : BaseRequest, IRequest<Result<PaginatedList<MenuViewModel>>>
    {
        private MenuGetAllQuery()
        {

        }
        public MenuGetAllQuery(PublicViewModel viewModel)
        {
            InputViewModel = viewModel;
        }
        public PublicViewModel InputViewModel { get; private set; }

    }

    public class MenuGetAllQueryHandler : BaseRequestHandler<MenuGetAllQuery, Result<PaginatedList<MenuViewModel>>>
    {
        private readonly IMenuRepository _MenuRepository;


        public MenuGetAllQueryHandler(
            IMenuRepository MenuRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _MenuRepository = MenuRepository;
        }

        protected async override Task<Result<PaginatedList<MenuViewModel>>> HandleRequestAsync(MenuGetAllQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<PaginatedList<MenuViewModel>>();
            var query = _unitOfWork.Menus.GetAll.Where(q=> q.MenuId > 1).AsNoTracking();
            var viewModel = query.ProjectTo<MenuViewModel>(_mapper.ConfigurationProvider, cancellationToken);

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
                Expression<Func<MenuViewModel, bool>> delegateQuery = ExpressionBuilder.GetExpression<MenuViewModel>(filters);
                var response = await viewModel
                  .Where(delegateQuery)
                  .PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);
                return result.WithValue(response).ConvertToDtatResult();
            }
            else
            {
                var MenuViewModel = await viewModel
                     .PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);

                return result.WithValue(MenuViewModel).ConvertToDtatResult();
            }
            
        }
    }
}
