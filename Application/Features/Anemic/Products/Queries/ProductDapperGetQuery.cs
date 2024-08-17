using Application.Common.Interfaces.Repository.Anemic.Base;
using AutoMapper;
using Dapper.Oracle;
using Domain.Constants;
using MediatR;
using Nazm.Results;
using NazmMapping.Mappings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Products;

namespace Application.Features.Anemic.Products.Queries
{
    public class ProductDapperGetQuery : BaseRequest, IRequest<Result<PaginatedList<ProductDapperViewModel>>>
    {
        public ProductDapperGetQuery()
        {

        }
        public ProductDapperGetQuery(ProductInputParamsViewModel viewModel)
        {
            InputViewModel = viewModel;
        }
        public ProductInputParamsViewModel InputViewModel { get; private set; }
    }

    public class ProductDapperGetQueryHandler : BaseRequestHandler<ProductDapperGetQuery, Result<PaginatedList<ProductDapperViewModel>>>
    {

        public ProductDapperGetQueryHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
        }

        protected async override Task<Result<PaginatedList<ProductDapperViewModel>>> HandleRequestAsync(ProductDapperGetQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<PaginatedList<ProductDapperViewModel>>();

            var dyParam = new OracleDynamicParameters();
            dyParam.Add(":P_ACNT_NO", input.InputViewModel.P_ACNT_NO, OracleMappingType.Int32, ParameterDirection.Input);
            dyParam.Add(":P_TR_DT", input.InputViewModel.P_TR_DT, OracleMappingType.Int32, ParameterDirection.Input);
            dyParam.Add(":P_STS", input.InputViewModel.P_STS, OracleMappingType.Int32, ParameterDirection.Input);
            dyParam.Add(name: ":P_TTR", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);


            var list = await _unitOfWork.ProductDapper.GetListAsync<ProductDapperViewModel>(
                dyParam,
                "PROC_GMBL_REF", Domain.Enums.DatabaseTypeEnum.Oracle);

            var count = list?.Count;
            var response = list?.PaginatedListSql(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, count ?? 0);

            return result.WithValue(response).ConvertToDtatResult();
        }
    }
}
