using Application.Common.Extensions;
using Application.Common;
using Application.Common.Interfaces.Repository.Anemic.Base;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;
using NazmMapping.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Controllers;
using ViewModels.Shared;
using static Application.Common.GridHelper;
using ViewModels.RoleManages;

namespace Application.Features.Anemic.Roles.Queries
{

    public class RoleGetAllQuery : BaseRequest, IRequest<Result<PaginatedList<RoleManageViewModel>>>
    {
        private RoleGetAllQuery()
        {

        }
        public RoleGetAllQuery(PublicViewModel viewModel)
        {
            InputViewModel = viewModel;
        }
        public PublicViewModel InputViewModel { get; private set; }
    }

    public class RoleGetAllQueryHandler : BaseRequestHandler<RoleGetAllQuery, Result<PaginatedList<RoleManageViewModel>>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleGetAllQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            RoleManager<IdentityRole> roleManager) : base(unitOfWork, mapper)
        {
            _roleManager = roleManager;
        }

        protected override async Task<Result<PaginatedList<RoleManageViewModel>>> HandleRequestAsync(RoleGetAllQuery input, CancellationToken cancellationToken)
        {

            
            var result = new FluentResults.Result<PaginatedList<RoleManageViewModel>>();
            var query = _roleManager.Roles;
            var viewModel = query.ProjectTo<RoleManageViewModel>(_mapper.ConfigurationProvider, cancellationToken);

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
                Expression<Func<RoleManageViewModel, bool>> delegateQuery = ExpressionBuilder.GetExpression<RoleManageViewModel>(filters);
                var response = await viewModel
                  .Where(delegateQuery)
                  .PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);
                return result.WithValue(response).ConvertToDtatResult();
            }
            else
            {
                var IdentityRole = await viewModel
                     .PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);

                return result.WithValue(IdentityRole).ConvertToDtatResult();
            }
        }
    }
}
