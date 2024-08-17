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
using ViewModels.ActionMethods;
using ViewModels.Shared;
using static Application.Common.GridHelper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Features.Anemic.ActionMethods.Queries
{

    public class ActionMethodGetAllJoinQuery : BaseRequest, IRequest<Result<PaginatedList<ActionMethodViewModel>>>
    {
        private ActionMethodGetAllJoinQuery()
        {

        }
        public ActionMethodGetAllJoinQuery(PublicViewModel viewModel)
        {
            InputViewModel = viewModel;
        }
        public PublicViewModel InputViewModel { get; private set; }

    }

    public class ActionMethodGetAllJoinQueryHandler : BaseRequestHandler<ActionMethodGetAllJoinQuery, Result<PaginatedList<ActionMethodViewModel>>>
    {
        private readonly IActionMethodRepository _ActionMethodRepository;


        public ActionMethodGetAllJoinQueryHandler(
            IActionMethodRepository ActionMethodRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _ActionMethodRepository = ActionMethodRepository;
        }

        protected async override Task<Result<PaginatedList<ActionMethodViewModel>>> HandleRequestAsync(ActionMethodGetAllJoinQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<PaginatedList<ActionMethodViewModel>>();
            var query = _unitOfWork.ActionMethods.GetAll.Include(s => s.Controller).AsNoTracking();
            var viewModel = query.ProjectTo<ActionMethodViewModel>(_mapper.ConfigurationProvider, cancellationToken);

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
                Expression<Func<ActionMethodViewModel, bool>> delegateQuery = ExpressionBuilder.GetExpression<ActionMethodViewModel>(filters);
                var response = await viewModel
                  .Where(delegateQuery)
                  .PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);
                return result.WithValue(response).ConvertToDtatResult();
            }
            else
            {
                var ActionMethodViewModel = await viewModel
                      .PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);

                return result.WithValue(ActionMethodViewModel).ConvertToDtatResult();
            }
            
        }
    }
}
