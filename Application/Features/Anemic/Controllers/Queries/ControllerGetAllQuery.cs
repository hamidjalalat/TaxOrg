using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using NazmMapping.Mappings;
using ViewModels.Controllers;
using Nazm.Results;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using ViewModels.Shared;
using Application.Common.Extensions;
using Application.Common;
using static Application.Common.GridHelper;
using System.Linq.Expressions;

namespace Application.Features.Anemic.Controllers.Queries
{

    public class ControllerGetAllQuery : BaseRequest, IRequest<Result<PaginatedList<ControllerViewModel>>>
    {
        private ControllerGetAllQuery()
        {

        }
        public ControllerGetAllQuery(PublicViewModel viewModel)
        {
            InputViewModel = viewModel;
        }
        public PublicViewModel InputViewModel { get; private set; }
    }

    public class ControllerGetAllQueryHandler : BaseRequestHandler<ControllerGetAllQuery, Result<PaginatedList<ControllerViewModel>>>
    {
        private readonly IControllerRepository _ControllerRepository;


        public ControllerGetAllQueryHandler(
            IControllerRepository ControllerRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _ControllerRepository = ControllerRepository;
        }

        protected async override Task<Result<PaginatedList<ControllerViewModel>>> HandleRequestAsync(ControllerGetAllQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<PaginatedList<ControllerViewModel>>();
            var query = _unitOfWork.Controllers.GetAll.AsNoTracking();
            var viewModel = query.ProjectTo<ControllerViewModel>(_mapper.ConfigurationProvider, cancellationToken);

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
                Expression<Func<ControllerViewModel, bool>> delegateQuery = ExpressionBuilder.GetExpression<ControllerViewModel>(filters);
                var response = await viewModel
                  .Where(delegateQuery)
                  .PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);
                return result.WithValue(response).ConvertToDtatResult();
            }
            else
            {
                var ControllerViewModel = await viewModel
                     .PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);

                return result.WithValue(ControllerViewModel).ConvertToDtatResult();
            }
           

        }
    }
}
