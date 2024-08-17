using Application.Common.Interfaces.Repository.Anemic.Base;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Anemic.Entities;
using Domain.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;
using NazmMapping.Mappings;

namespace Application.Features.Anemic.Products.Queries
{

    public class ProductGetQuery : BaseRequest, IRequest<Result<PaginatedList<Product>>>
    {
        public ProductGetQuery()
        {

        }
        public ProductGetQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        public int PageNumber { get; set; } = PublicConstants.PageNumber;
        public int PageSize { get; set; } = PublicConstants.PageSize;
    }

    public class ProductGetQueryHandler : BaseRequestHandler<ProductGetQuery, Result<PaginatedList<Product>>>
    {

        public ProductGetQueryHandler(
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
        }

        protected async override Task<Result<PaginatedList<Product>>> HandleRequestAsync(ProductGetQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<PaginatedList<Product>>();
            var response = _unitOfWork.Products.GetAll;
            
            var viewModel = await response
                     .PaginatedListAsync(input.PageNumber, input.PageSize, cancellationToken);

            return result.WithValue(viewModel).ConvertToDtatResult();
        }
    }
}
