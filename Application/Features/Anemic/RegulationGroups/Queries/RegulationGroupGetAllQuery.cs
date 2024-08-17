using Application.Common;
using Application.Common.Extensions;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using Application.Common.Security;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;
using NazmMapping.Mappings;
using System.Linq.Expressions;
using ViewModels;
using ViewModels.RegulationGroups;
using ViewModels.Shared;
using static Application.Common.GridHelper;

namespace Application.Features.Anemic.RegulationGroups.Queries
{
    //[Authorize]
    public class RegulationGroupGetAllQuery : BaseRequest, IRequest<Result<PaginatedList<RegulationGroupViewModel>>>
    {
        private RegulationGroupGetAllQuery()
        {

        }
        public RegulationGroupGetAllQuery(PublicViewModel viewModel)
        {
            //PageNumber = publicViewModel.PageNumber;
            //PageSize = publicViewModel.PageSize;
            //FilterParams = publicViewModel.FilterParams;
            //MenuId = publicViewModel.MenuId;
            InputViewModel = viewModel;
        }
        //public FilterParams? FilterParams { get; private set; }
        //public int PageNumber { get; private set; } = PublicConstants.PageNumber;
        //public int PageSize { get; private set; } = PublicConstants.PageSize;
        //public int? MenuId { get; private set; }
        public PublicViewModel InputViewModel { get; private set; }

    }

    public class RegulationGroupGetAllQueryHandler : BaseRequestHandler<RegulationGroupGetAllQuery, Result<PaginatedList<RegulationGroupViewModel>>>
    {
        private readonly IRegulationGroupRepository _RegulationGroupRepository;


        public RegulationGroupGetAllQueryHandler(
            IRegulationGroupRepository RegulationGroupRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _RegulationGroupRepository = RegulationGroupRepository;
        }

        protected async override Task<Result<PaginatedList<RegulationGroupViewModel>>> HandleRequestAsync(RegulationGroupGetAllQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<PaginatedList<RegulationGroupViewModel>>();
            var query = _unitOfWork.RegulationGroups.GetAll.OrderByDescending(s => s.Id).AsNoTracking();
            var viewModel = query.ProjectTo<RegulationGroupViewModel>(_mapper.ConfigurationProvider, cancellationToken);

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
                Expression<Func<RegulationGroupViewModel, bool>> delegateQuery = ExpressionBuilder.GetExpression<RegulationGroupViewModel>(filters);
                var response = await viewModel
                  .Where(delegateQuery)
                  .PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);
                return result.WithValue(response).ConvertToDtatResult();
            }
            else
            {
                var RegulationGroupViewModel = await viewModel
                     .PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);

                return result.WithValue(RegulationGroupViewModel).ConvertToDtatResult();
            }

        }
    }
}
