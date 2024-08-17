using Application.Common.Extensions;
using Application.Common;
using Application.Common.Interfaces.Repository.Anemic.Base;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;
using NazmMapping.Mappings;
using System.Linq.Expressions;
using static Application.Common.GridHelper;
using ViewModels.LoginHistories;

namespace Application.Features.Anemic.LoginHistories.Queries
{

    public class LoginHistoryGetQuery : BaseRequest, IRequest<Result<PaginatedList<LoginHistoryViewModel>>>
    {
        private LoginHistoryGetQuery()
        {

        }
        public LoginHistoryGetQuery(LoginHistoryInputParamsViewModel viewModel)
        {
            InputViewModel = viewModel;
        }
        public LoginHistoryInputParamsViewModel InputViewModel { get; private set; }

    }

    public class AutoLoginHistoryGetQueryHandler : BaseRequestHandler<LoginHistoryGetQuery, Result<PaginatedList<LoginHistoryViewModel>>>
    {
        public AutoLoginHistoryGetQueryHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {

        }

        protected async override Task<Result<PaginatedList<LoginHistoryViewModel>>> HandleRequestAsync(LoginHistoryGetQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<PaginatedList<LoginHistoryViewModel>>();
            var viewModel = _unitOfWork.LoginHistories.GetAll
                .Include(s => s.User)
                .Where(s => s.UserId == input.InputViewModel.UserId || input.InputViewModel.UserId == null)
                .Select(s => new LoginHistoryViewModel
                {
                    Id = s.Id,
                    UserFirstName = s.User == null ? "" : s.User.FirstName,
                    UserLastName = s.User == null ? "" : s.User.LastName,
                    IPAddress = s.IPAddress,
                    ComputerName = s.ComputerName,
                    LogDate = s.LogDate,
                    HistoryType = s.HistoryType,
                });


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
                Expression<Func<LoginHistoryViewModel, bool>> delegateQuery = ExpressionBuilder.GetExpression<LoginHistoryViewModel>(filters);
                var response = await viewModel
                  .Where(delegateQuery)
                  .PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);
                return result.WithValue(response).ConvertToDtatResult();
            }
            else
            {
                var EntityAutoHistory = await viewModel
                      .OrderByDescending(s => s.LogDate)
                      .PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);

                return result.WithValue(EntityAutoHistory).ConvertToDtatResult();
            }

        }
    }
}
