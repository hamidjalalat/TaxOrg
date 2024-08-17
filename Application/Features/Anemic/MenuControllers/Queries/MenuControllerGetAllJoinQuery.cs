using Application.Common;
using Application.Common.Extensions;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;
using NazmMapping.Mappings;
using System.Linq.Expressions;
using ViewModels.MenuControllers;
using ViewModels.Shared;
using static Application.Common.GridHelper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Features.Anemic.MenuControllers.Queries
{

    public class MenuControllerGetAllJoinQuery : BaseRequest, IRequest<Result<PaginatedList<MenuControllerViewModel>>>
    {
        private MenuControllerGetAllJoinQuery()
        {

        }
        public MenuControllerGetAllJoinQuery(PublicViewModel viewModel)
        {
            InputViewModel = viewModel;
        }
        public PublicViewModel InputViewModel { get; private set; }

    }

    public class MenuControllerGetAllJoinQueryHandler : BaseRequestHandler<MenuControllerGetAllJoinQuery, Result<PaginatedList<MenuControllerViewModel>>>
    {
        private readonly IMenuControllerRepository _MenuControllerRepository;


        public MenuControllerGetAllJoinQueryHandler(
            IMenuControllerRepository MenuControllerRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _MenuControllerRepository = MenuControllerRepository;
        }

        protected async override Task<Result<PaginatedList<MenuControllerViewModel>>> HandleRequestAsync(MenuControllerGetAllJoinQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<PaginatedList<MenuControllerViewModel>>();
            var query = _unitOfWork.MenuControllers.GetAll.Include(s => s.Menu).Include(s => s.Controller).AsNoTracking();
            var viewModel = query.ProjectTo<MenuControllerViewModel>(_mapper.ConfigurationProvider, cancellationToken);

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
                Expression<Func<MenuControllerViewModel, bool>> delegateQuery = ExpressionBuilder.GetExpression<MenuControllerViewModel>(filters);
                var response = await viewModel
                  .Where(delegateQuery)
                  .PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);
                return result.WithValue(response).ConvertToDtatResult();
            }
            else
            {
                var MenuControllerViewModel = await viewModel
                     .PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);

                return result.WithValue(MenuControllerViewModel).ConvertToDtatResult();
            }
           
        }
    }
}
