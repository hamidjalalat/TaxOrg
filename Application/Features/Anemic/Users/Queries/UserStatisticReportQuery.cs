using Application.Common.Interfaces.Repository.Anemic.Base;
using AutoMapper;
using Dapper;
using MediatR;
using Nazm.Results;
using NazmMapping.Mappings;
using System.Data;
using ViewModels.Users;

namespace Application.Features.Anemic.Users.Queries
{
  
    public class UserStatisticReportQuery : BaseRequest, IRequest<Result<PaginatedList<UserManageStatisticReportViewModel>>>
    {
        public UserStatisticReportQuery()
        {

        }
        public UserStatisticReportQuery(UserManageStatisticReportInputParamsViewModel inputParamsViewModel)
        {
			InputParamsViewModel = inputParamsViewModel;

        }
        public UserManageStatisticReportInputParamsViewModel InputParamsViewModel { get; private set; }


    }

    public class UserStatisticReportQueryHandler : BaseRequestHandler<UserStatisticReportQuery, Result<PaginatedList<UserManageStatisticReportViewModel>>>
    {
        public UserStatisticReportQueryHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
        }

        protected async override Task<Result<PaginatedList<UserManageStatisticReportViewModel>>> HandleRequestAsync(UserStatisticReportQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<PaginatedList<UserManageStatisticReportViewModel>>();

			string FilterParams_SortBy = null;
			string FilterParams_Filter = null;
			if (input.InputParamsViewModel.FilterParams != null && input.InputParamsViewModel.FilterParams.SortBy != null)
			{
				FilterParams_SortBy += $"{input.InputParamsViewModel.FilterParams?.SortBy}, ";
				FilterParams_SortBy = FilterParams_SortBy.Trim();
				FilterParams_SortBy = FilterParams_SortBy.TrimEnd(',');
			}
			if (input.InputParamsViewModel.FilterParams != null && input.InputParamsViewModel.FilterParams.Filter != null && input.InputParamsViewModel.FilterParams.Filter.Count > 0)
			{
				foreach (var item in input.InputParamsViewModel.FilterParams.Filter)
				{
					FilterParams_Filter += $"{item.Field} {FilterSortConversion.GetInstance().FilterSortToWhereOrderInDapper(item.Operator, item.Value)} AND ";
				}

				FilterParams_Filter = FilterParams_Filter.Trim();
				FilterParams_Filter = FilterParams_Filter.Substring(0, FilterParams_Filter.Length - 3);
			}

			DynamicParameters ObjParm = new DynamicParameters();
			ObjParm.Add("@FilterParams_SortBy", FilterParams_SortBy);
			ObjParm.Add("@FilterParams_Filter", FilterParams_Filter);
			ObjParm.Add("@UserId", input.InputParamsViewModel.UserId);
			ObjParm.Add("@FromDate", input.InputParamsViewModel.FromDate);
            ObjParm.Add("@ToDate", input.InputParamsViewModel.ToDate);
            ObjParm.Add("@PageNumber", input.InputParamsViewModel.PageNumber);
            ObjParm.Add("@PageSize", input.InputParamsViewModel.PageSize);
            ObjParm.Add("@TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var list = await _unitOfWork.UserDapper.GetListAsync<UserManageStatisticReportViewModel>(
                ObjParm,
                "usp_Report_UserStatistic", Domain.Enums.DatabaseTypeEnum.SQL);
            var count = ObjParm.Get<int>("@TotalCount");
            var response = list?.PaginatedListSql(input.InputParamsViewModel.PageNumber, input.InputParamsViewModel.PageSize, count);
            return result.WithValue(response).ConvertToDtatResult();
        }
    }
}
