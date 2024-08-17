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
using ViewModels.Histories;

namespace Application.Features.Anemic.Histories.Queries
{

	public class HistoryGetQuery : BaseRequest, IRequest<Result<PaginatedList<HistoryViewModel>>>
	{
		private HistoryGetQuery()
		{

		}
		public HistoryGetQuery(HistoryInputParamsViewModel viewModel)
		{
			InputViewModel = viewModel;
		}
		public HistoryInputParamsViewModel InputViewModel { get; private set; }

	}

	public class AutoHistoryGetQueryHandler : BaseRequestHandler<HistoryGetQuery, Result<PaginatedList<HistoryViewModel>>>
	{
		public AutoHistoryGetQueryHandler(
			IMapper mapper,
			IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
		{

		}

		protected async override Task<Result<PaginatedList<HistoryViewModel>>> HandleRequestAsync(HistoryGetQuery input, CancellationToken cancellationToken)
		{
			var result = new FluentResults.Result<PaginatedList<HistoryViewModel>>();
			var viewModel = _unitOfWork.AutoHistories.GetAll
				.Include(s => s.User)
				.Where(s => s.TableName != "LoginHistories" && (s.UserId == input.InputViewModel.UserId || input.InputViewModel.UserId == null))
			.Select(s => new HistoryViewModel
			{
				Id = s.Id,
				UserFirstName = ((s.User == null) ? "" : s.User.FirstName),
				UserLastName = ((s.User == null) ? "" : s.User.LastName),
				IPAddress = s.IPAddress,
				ComputerName = s.ComputerName,
				CreateDate = s.Created,
				IsDeleted = s.IsDeleted,
				HistoryActionType = s.HistoryActionType,
				Kind = s.Kind,
				TableName = s.TableName

			});
			// var viewModel = query.ProjectTo<HistoryViewModel>(_mapper.ConfigurationProvider);
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
				Expression<Func<HistoryViewModel, bool>> delegateQuery = ExpressionBuilder.GetExpression<HistoryViewModel>(filters);
				var response = await viewModel
				  .Where(delegateQuery)
				  .PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);
				return result.WithValue(response).ConvertToDtatResult();
			}
			else
			{
				var EntityAutoHistory = await viewModel
					  .OrderByDescending(s => s.CreateDate)
					  .PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);

				return result.WithValue(EntityAutoHistory).ConvertToDtatResult();
			}

		}
	}
}
