using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NazmMapping.Mappings;
using ViewModels.MenuControllerActions;
using Nazm.Results;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using Application.Common;
using static Application.Common.GridHelper;
using System.Linq.Expressions;
using ViewModels.Shared;
using Application.Common.Extensions;

namespace Application.Features.Anemic.MenuControllerActions.Queries
{

    public class MenuControllerActionGetAllJoinQuery : BaseRequest, IRequest<Result<PaginatedList<MenuControllerActionViewModel>>>
    {
        private MenuControllerActionGetAllJoinQuery()
        {

        }
        public MenuControllerActionGetAllJoinQuery(PublicViewModel viewModel)
        {
            InputViewModel = viewModel;
        }
        public PublicViewModel InputViewModel { get; private set; }

    }

    public class MenuControllerActionGetAllJoinQueryHandler : BaseRequestHandler<MenuControllerActionGetAllJoinQuery, Nazm.Results.Result<PaginatedList<MenuControllerActionViewModel>>>
    {
        private readonly IMenuControllerActionRepository _MenuControllerActionRepository;


        public MenuControllerActionGetAllJoinQueryHandler(
            IMenuControllerActionRepository MenuControllerActionRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _MenuControllerActionRepository = MenuControllerActionRepository;
        }

        protected async override Task<Result<PaginatedList<MenuControllerActionViewModel>>> HandleRequestAsync(MenuControllerActionGetAllJoinQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<PaginatedList<MenuControllerActionViewModel>>();
            var query = _unitOfWork.MenuControllerActions.GetAll
            .Include(s => s.ActionMethod)
                .Include(s => s.MenuController).ThenInclude(s => s.Controller)
                .AsNoTracking();
            var viewModel = query.ProjectTo<MenuControllerActionViewModel>(_mapper.ConfigurationProvider, cancellationToken);

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
                Expression<Func<MenuControllerActionViewModel, bool>> delegateQuery = ExpressionBuilder.GetExpression<MenuControllerActionViewModel>(filters);
                var response = await viewModel
                  .Where(delegateQuery)
                  .PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);
                return result.WithValue(response).ConvertToDtatResult();
            }
            else
            {
                var MenuControllerActionViewModel = await viewModel
                      .PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);

                return result.WithValue(MenuControllerActionViewModel).ConvertToDtatResult();
            }
        }
    }
}
