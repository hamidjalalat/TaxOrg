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
using ViewModels.Nazm_tspagents;
using ViewModels.Shared;
using static Application.Common.GridHelper;

namespace Application.Features.Anemic.Nazm_tspagents.Queries
{
    public class Nazm_tspagentGetAllQuery : BaseRequest, IRequest<Result<PaginatedList<Nazm_tspagentViewModel>>>
    {
        private Nazm_tspagentGetAllQuery()
        {

        }
        public Nazm_tspagentGetAllQuery(PublicViewModel viewModel)
        {
       
            InputViewModel = viewModel;
        }
        public PublicViewModel InputViewModel { get; private set; }

    }

    public class Nazm_tspagentGetAllQueryHandler : BaseRequestHandler<Nazm_tspagentGetAllQuery, Result<PaginatedList<Nazm_tspagentViewModel>>>
    {
        private readonly INazm_tspagentRepository _Nazm_tspagentRepository;

        public Nazm_tspagentGetAllQueryHandler( INazm_tspagentRepository Nazm_tspagentRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _Nazm_tspagentRepository = Nazm_tspagentRepository;
        }

        protected async override Task<Result<PaginatedList<Nazm_tspagentViewModel>>> HandleRequestAsync(Nazm_tspagentGetAllQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<PaginatedList<Nazm_tspagentViewModel>>();

            var query = _unitOfWork.Nazm_tspagents.GetAll.OrderByDescending(s => s.id).AsNoTracking();

            var viewModel = query.ProjectTo<Nazm_tspagentViewModel>(_mapper.ConfigurationProvider, cancellationToken);

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

                Expression<Func<Nazm_tspagentViewModel, bool>> delegateQuery = ExpressionBuilder.GetExpression<Nazm_tspagentViewModel>(filters);

                var response = await viewModel
                    .Where(delegateQuery)
                  .PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);

                return result.WithValue(response).ConvertToDtatResult();
            }

            else
            {
                var Nazm_tspagentViewModel = await viewModel
                     .PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);

                return result.WithValue(Nazm_tspagentViewModel).ConvertToDtatResult();
            }

        }
    }
}
